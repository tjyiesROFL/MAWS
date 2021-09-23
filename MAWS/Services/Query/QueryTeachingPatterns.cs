using MAWS.Models;
using MAWS.IntermediateData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Blazor.Extensions.Canvas.WebGL;

namespace MAWS.Services
{
    public class QueryTeachingPatterns
    {
        private readonly ApplicationDbContext _db;

        public QueryTeachingPatterns(ApplicationDbContext db)
        {
            _db = db;
        }

        //function to create all teaching activities...
        public bool Add(IntermediateTeachingPattern intrTeachingPattern)
        {

            var unitOffering = _db.UnitOffering
                .Where(r => r.UnitOfferingID == intrTeachingPattern.UnitOfferingID)
                .FirstOrDefault();

            var unitCoordinator = FindAcademicStaff(unitOffering);

            _db.Entry(unitOffering)
                .Collection(unitOffering => unitOffering.TeachingPatternList)
                .Load();
            _db.Entry(unitCoordinator)
                .Collection(unitOffering => unitOffering.TeachingPatternList)
                .Load();
            var unit = FindUnit(unitOffering);



            var teachingPattern = new TeachingPattern();

            teachingPattern.TeachingPatternID = unitOffering.UnitCode + unitOffering.Year + unitOffering.TeachingPeriod;
            teachingPattern.UnitCode = unitOffering.UnitCode;
            teachingPattern.Year = unitOffering.Year;
            teachingPattern.TeachingPeriod = unitOffering.TeachingPeriod;
            teachingPattern.OfferingType = unitOffering.OfferingType;
            teachingPattern.TotalEnrolments = intrTeachingPattern.TotalEnrolments;
            teachingPattern.ExternalEnrolments = intrTeachingPattern.ExternalEnrolments;
            teachingPattern.EnrolmentStatus = intrTeachingPattern.EnrolmentStatus;
            teachingPattern.WOCT_HrsPerSessionFIRST = intrTeachingPattern.WOCT_HrsPerSessionFIRST;
            teachingPattern.WOCT_SessionsPerWeekFIRST = intrTeachingPattern.WOCT_SessionsPerWeekFIRST;
            teachingPattern.WOCT_HrsPerSessionREPEAT = intrTeachingPattern.WOCT_HrsPerSessionREPEAT;
            teachingPattern.WOCT_SessionsPerWeekREPEAT = intrTeachingPattern.WOCT_SessionsPerWeekREPEAT;
            teachingPattern.SGT_ClassSize = intrTeachingPattern.SGT_ClassSize;
            teachingPattern.SGT_HrsPerSession = intrTeachingPattern.SGT_HrsPerSession;
            teachingPattern.SGT_SessionsPerWeek = intrTeachingPattern.SGT_SessionsPerWeek;
            teachingPattern.UC_TNE_Affiliates = intrTeachingPattern.UC_TNE_Affiliates;
            teachingPattern.PU_GroupSize = intrTeachingPattern.PU_GroupSize;
            teachingPattern.PU_StaffAsClientQty = intrTeachingPattern.PU_StaffAsClientQty;
            teachingPattern.PU_StaffAsClientTNE_Qty = intrTeachingPattern.PU_StaffAsClientTNE_Qty;
            teachingPattern.UC_New = intrTeachingPattern.UC_New;
            teachingPattern.UD_Type = intrTeachingPattern.UD_Type;
            teachingPattern.UD_DiscretionHrs = intrTeachingPattern.UD_DiscretionHrs;
            teachingPattern.UD_DiscretionHrsComment = intrTeachingPattern.UD_DiscretionHrsComment;
            teachingPattern.NoTeachingWeeks = intrTeachingPattern.NoTeachingWeeks;
            teachingPattern.ActiveFlag = intrTeachingPattern.ActiveFlag;
            teachingPattern.UnitOfferingID = intrTeachingPattern.UnitOfferingID;

            unitCoordinator.TeachingPatternList.Add(teachingPattern);

            unitOffering.TeachingPatternList.Add(teachingPattern);

            GenerateActivities(unitCoordinator, teachingPattern, unitOffering, unit);

            return true;

        }

        //should move in academicstaffservice
        public async Task<List<string>> GetIdListAsync()
        {
            return await _db.UnitOffering.Select(a => a.UnitOfferingID).ToListAsync();
        }



        private AcademicStaff FindAcademicStaff(UnitOffering unitOffcering)
        {

            foreach (var entry in _db.AcademicStaff.ToList())
            {

                _db.Entry(entry)
                    .Collection(unitOffering => unitOffering.UnitOfferingList)
                    .Load();

                if (entry.UnitOfferingList != null)
                {
                    if (entry.UnitOfferingList.Contains(unitOffcering))
                    {
                        return entry;
                    }
                }
            }

            return null;

        }

        private  Unit FindUnit(UnitOffering unitOffering)
        {

            foreach (var entry in _db.Unit.ToList())
            {

                 _db.Entry(entry)
                    .Collection(unitOffering => unitOffering.UnitOfferingList)
                    .Load();

                if (entry.UnitOfferingList != null)
                {
                    if (entry.UnitOfferingList.Contains(unitOffering))
                    {
                        return entry;
                    }
                }
            }

            return null;

        }

        /// this function should create required activities and automatically assign all the unit coordinator
        public void GenerateActivities(AcademicStaff _unitCoordinator, TeachingPattern _teachingPattern, UnitOffering _unitOffering, Unit _unit)
        {

            var discipline = _db.Discipline.FirstOrDefault();

            List<TeachingActivityAssignment> taa = new List<TeachingActivityAssignment>();

            List<TeachingActivity> teachingActivities = new List<TeachingActivity>();

            //some code to create activities depending on flags 


            //function params:     UnitCode           TeachingPeriod            activity name             sessions per week         hours per session

            if (_unitOffering.WOCT_Flag) //whole cohort shiznick
            {



                TeachingActivityAssignment ta1 = new TeachingActivityAssignment();

                string title = "Whole of Cohort Teaching - First";

                int activityQTY = _teachingPattern.WOCT_SessionsPerWeekFIRST*12;

                double ActivityHrs = _teachingPattern.WOCT_HrsPerSessionFIRST;

                var activity = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                    title, activityQTY, ActivityHrs);
                activity.Transferable = true;

                ta1.ActivityHrs = (int)(activityQTY * ActivityHrs);
                ta1.WorkloadHrs = (int)(activityQTY * discipline.WOCT_FirstHrsPH);
                ta1.Activity = title;
                ta1.UnitCode = _unitOffering.UnitCode;
                ta1.Year = _unitOffering.Year;
                ta1.TeachingPeriod = _unitOffering.TeachingPeriod;

                taa.Add(ta1);
                teachingActivities.Add(activity);


                if (_teachingPattern.WOCT_SessionsPerWeekREPEAT > 0)
                {

                    string title2 = "Whole of Cohort Teaching - Repeat";

                    TeachingActivityAssignment ta3 = new TeachingActivityAssignment();

                    int activityQTY2 = _teachingPattern.WOCT_SessionsPerWeekREPEAT;

                    double ActivityHrs2 = _teachingPattern.WOCT_HrsPerSessionREPEAT;

                    var activity2 = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                        title2, activityQTY2 * 12, ActivityHrs2);
                    activity2.Transferable = true;
                    ta3.ActivityHrs = (int)(activityQTY * ActivityHrs2);
                    ta3.WorkloadHrs = (int)(activityQTY * discipline.WOCT_RepeatHRsPH);
                    ta3.Activity = title2;
                    ta3.UnitCode = _unitOffering.UnitCode;
                    ta3.Year = _unitOffering.Year;
                    ta3.TeachingPeriod = _unitOffering.TeachingPeriod;
                    taa.Add(ta3);
                    teachingActivities.Add(activity2);

                }

            }
            if (_unitOffering.SGT_Flag) //small group teaching shiznick
            {

                string title2 = "Small Group Teaching";



                int activityMultiple = RoundUp(_teachingPattern.TotalEnrolments - _teachingPattern.ExternalEnrolments) / (_teachingPattern.SGT_ClassSize);

                int activityQTY2 = _teachingPattern.SGT_SessionsPerWeek;

                double ActivityHrs2 = _teachingPattern.SGT_HrsPerSession;



                var activity2 = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                    title2, activityQTY2 * 12, ActivityHrs2);
                activity2.Transferable = true;
                TeachingActivityAssignment ta1 = new TeachingActivityAssignment();

                ta1.ActivityHrs = (int)(activityMultiple * ActivityHrs2 * activityQTY2);

                ta1.WorkloadHrs = (int)((activityMultiple)* (activityQTY2*12)*discipline.SGT_SubsequentHrsPH+(discipline.SGT_FirstHrsPH*12));
                ta1.Activity = title2;
                ta1.UnitCode = _unitOffering.UnitCode;
                ta1.Year = _unitOffering.Year;
                ta1.TeachingPeriod = _unitOffering.TeachingPeriod;


                teachingActivities.Add(activity2);
                taa.Add(ta1);
                //--------------------//

                TeachingActivityAssignment ta2 = new TeachingActivityAssignment();

                string title = "Small Group Marking";


                int activityQTY = _teachingPattern.SGT_ClassSize;

                int activityMultipl = RoundUp(_teachingPattern.TotalEnrolments - _teachingPattern.ExternalEnrolments) / (_teachingPattern.SGT_ClassSize);

                double ActivityHrs = discipline.SGT_MarkingHrsPS;

                var activity = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                    title, activityQTY, ActivityHrs);
                activity.Transferable = true;
                ta2.ActivityHrs = (int)( ActivityHrs * activityQTY); //activityMultipl
                ta2.WorkloadHrs = ta2.ActivityHrs;
                ta2.Activity = title;
                ta2.UnitCode = _unitOffering.UnitCode;
                ta2.Year = _unitOffering.Year;
                ta2.TeachingPeriod = _unitOffering.TeachingPeriod;

                teachingActivities.Add(activity);
                taa.Add(ta2);
            }
            //if (_unit.ProjectFlag) 
            //{

            //    var activityQuantity = RoundUp(_teachingPattern.TotalEnrolments / _teachingPattern.PU_GroupSize);


            //    var teachingAct = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
            //        "Project Unit - Supervisor", activityQuantity, discipline.SGT_MarkingHrsPS);

            //    teachingActivities.Add(teachingAct);

            //    if(_teachingPattern.PU_StaffAsClientQty>0)
            //    {
            //        var teachingA = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
            //            "Project Unit - Staff as Client", _teachingPattern.PU_StaffAsClientQty, discipline.SGT_MarkingHrsPS);

            //        teachingActivities.Add(teachingA);
            //    }
            //}

            AutomaticallyAssignActivities(_unitCoordinator, _unitOffering, teachingActivities, taa);



        }

        private TeachingActivity CreateTransferable(string unitCode, string teachingPeriod, string activityName, int activityQuantity, double ActivityHours)
        {
            TeachingActivity teachingActivity = new TeachingActivity
            {
                UnitCode = unitCode,
                Year = 2020,
                TeachingPeriod = teachingPeriod,
                ActivityWeeks = 12,
                Transferable = false,
                Activity = activityName,
                ActivityQty = activityQuantity,
                ActivityHrs = (int)ActivityHours,
                ActiveFlag = true
            };
            return teachingActivity;
        }

        private int RoundUp(double num)
        {
            return (int)(num + 0.5);
        }

        private void AutomaticallyAssignActivities(AcademicStaff _unitCoordinator, UnitOffering _unitOffering, List<TeachingActivity> _teachingActivities, List<TeachingActivityAssignment> _teachingActivitiesAssignment)
        {

            if (_unitCoordinator.TeachingActivityList == null)
            {
                _unitCoordinator.TeachingActivityList = new List<TeachingActivity>();
            }

            if (_unitCoordinator.TeachingActivityAssignmentList == null)
            {
                _unitCoordinator.TeachingActivityAssignmentList = new List<TeachingActivityAssignment>();
            }

            _unitOffering.TeachingActivityList = new List<TeachingActivity>();
            
            _unitCoordinator.TeachingActivityList.AddRange(_teachingActivities);
            _unitOffering.TeachingActivityList.AddRange(_teachingActivities);
            _unitCoordinator.TeachingActivityAssignmentList.AddRange(_teachingActivitiesAssignment);

            try
            {
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("error msg: " + e.InnerException);
            }
        }

        public List<AcademicStaff> GetActivitiesAsync()
        {
            List<AcademicStaff> list = new List<AcademicStaff>();


            var staffList = _db.AcademicStaff.ToList();

            foreach (var entry in staffList)
            {
                _db.Entry(entry)
                    .Collection(unitOffering => unitOffering.TeachingActivityAssignmentList)
                    .Load();

                if (entry.TeachingActivityAssignmentList != null)
                {
                    list.Add(entry);

                }
            }
            return list;

        }

        public void ReassignTeaching(AcademicStaff staff, TeachingActivityAssignment temp, string staffID)
        {


            staff.TeachingActivityAssignmentList.Remove(temp);

            var newStaff = _db.AcademicStaff.Where(a => a.AcademicStaffID == staffID).FirstOrDefault();

            _db.Entry(newStaff)
                .Collection(unitOffering => unitOffering.TeachingActivityAssignmentList)
                .Load();

            if(newStaff.TeachingActivityAssignmentList == null)
            {
                newStaff.TeachingActivityAssignmentList = new List<TeachingActivityAssignment>();
            }
            newStaff.TeachingActivityAssignmentList.Add(temp);


            try
            {
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e.InnerException);
            }

        }

        public async Task<List<string>> GetIdList()
        {
            return await _db.AcademicStaff.Select(a => a.AcademicStaffID).ToListAsync();
        }
    }
}


