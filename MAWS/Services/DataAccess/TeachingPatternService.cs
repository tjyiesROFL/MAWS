using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using MAWS.IntermediateData;
using MAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace MAWS.Services.DataAccess
{
    // not sure if its a best practice to have other classes int IDataAccessServices<T> ?
    public class TeachingActivityService : IDataAccessServices<IntermediateTeachingPattern>
    {
        public string ClassName => nameof(TeachingPatternService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<TeachingPattern, string>> _teachingPatternTupleList = new List<Tuple<TeachingPattern, string>>();


        public TeachingActivityService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }


        public  async Task<List<IntermediateTeachingPattern>> GetListAsync()
        {
            List<IntermediateTeachingPattern> intrTeachingPatternList = new List<IntermediateTeachingPattern>();

            foreach (var teachingPattern in await _db.TeachingPattern.ToListAsync())
            {
                if (teachingPattern != null)
                {

                    var intrTeachingPattern = new IntermediateTeachingPattern();

                    intrTeachingPattern.TeachingPatternID = teachingPattern.TeachingPatternID;
                    intrTeachingPattern.UnitCode = teachingPattern.UnitCode;
                    intrTeachingPattern.Year = teachingPattern.Year;
                    intrTeachingPattern.TeachingPeriod = teachingPattern.TeachingPeriod;
                    intrTeachingPattern.OfferingType = teachingPattern.OfferingType;
                    intrTeachingPattern.TotalEnrolments = teachingPattern.TotalEnrolments;
                    intrTeachingPattern.ExternalEnrolments = teachingPattern.ExternalEnrolments;
                    intrTeachingPattern.EnrolmentStatus = teachingPattern.EnrolmentStatus;
                    intrTeachingPattern.WOCT_HrsPerSessionFIRST = teachingPattern.WOCT_HrsPerSessionFIRST;
                    intrTeachingPattern.WOCT_SessionsPerWeekFIRST = teachingPattern.WOCT_SessionsPerWeekFIRST;
                    intrTeachingPattern.WOCT_HrsPerSessionREPEAT = teachingPattern.WOCT_HrsPerSessionREPEAT;
                    intrTeachingPattern.WOCT_SessionsPerWeekREPEAT = teachingPattern.WOCT_SessionsPerWeekREPEAT;
                    intrTeachingPattern.SGT_ClassSize = teachingPattern.SGT_ClassSize;
                    intrTeachingPattern.SGT_HrsPerSession = teachingPattern.SGT_HrsPerSession;
                    intrTeachingPattern.SGT_SessionsPerWeek = teachingPattern.SGT_SessionsPerWeek;
                    intrTeachingPattern.UC_TNE_Affiliates = teachingPattern.UC_TNE_Affiliates;
                    intrTeachingPattern.PU_GroupSize = teachingPattern.PU_GroupSize;
                    intrTeachingPattern.PU_StaffAsClientQty = teachingPattern.PU_StaffAsClientQty;
                    intrTeachingPattern.PU_StaffAsClientTNE_Qty = teachingPattern.PU_StaffAsClientTNE_Qty;
                    intrTeachingPattern.UC_New = teachingPattern.UC_New;
                    intrTeachingPattern.UD_Type = teachingPattern.UD_Type;
                    intrTeachingPattern.UD_DiscretionHrs = teachingPattern.UD_DiscretionHrs;
                    intrTeachingPattern.UD_DiscretionHrsComment = teachingPattern.UD_DiscretionHrsComment;
                    intrTeachingPattern.NoTeachingWeeks = teachingPattern.NoTeachingWeeks;
                    intrTeachingPattern.ActiveFlag = teachingPattern.ActiveFlag;
                    intrTeachingPattern.UnitOfferingID = teachingPattern.UnitOfferingID;


                    intrTeachingPatternList.Add(intrTeachingPattern);
                        
                
                }
            }
            return intrTeachingPatternList;
        }

        // GetAsync if needed?

        public async Task<bool> AddAsync(IntermediateTeachingPattern intrTeachingPattern)
        {


            var unitOffering = await _db.UnitOffering
                .Where(r => r.UnitOfferingID == intrTeachingPattern.UnitOfferingID)
                .FirstOrDefaultAsync();
            var unitCoordinator =  await findAcademicStaffAsync(unitOffering);

            await _db.Entry(unitOffering)
                .Collection(unitOffering => unitOffering.TeachingPatternList)
                .LoadAsync();
            await _db.Entry(unitCoordinator)
                .Collection(unitOffering => unitOffering.TeachingPatternList)
                .LoadAsync();
            var unit = await FindUnit(unitOffering);




            var teachingPattern = new TeachingPattern();

            teachingPattern.TeachingPatternID = intrTeachingPattern.TeachingPatternID;
            teachingPattern.UnitCode = intrTeachingPattern.UnitCode;
            teachingPattern.Year = intrTeachingPattern.Year;
            teachingPattern.TeachingPeriod = intrTeachingPattern.TeachingPeriod;
            teachingPattern.OfferingType = intrTeachingPattern.OfferingType;
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

        public async Task RemoveAsync(IntermediateTeachingPattern intermediateTeachingPattern)
        {

        }

        //should move in academicstaffservice
        public async Task<List<string>> GetIdListAsync()
        {
            return await _db.UnitOffering.Select(a => a.UnitOfferingID).ToListAsync();
        }

        public async Task UploadAsync(MemoryStream ms)
        {
            
        }

        private async Task AddTeachingPatternListAsync()
        {

        }

        private async Task<AcademicStaff> findAcademicStaffAsync(UnitOffering unitOffcering)
        {

            foreach (var entry in _db.AcademicStaff.ToList())
            {

                await _db.Entry(entry)
                    .Collection(unitOffering => unitOffering.UnitOfferingList)
                    .LoadAsync();

                if(entry.UnitOfferingList != null)
                {
                    if(entry.UnitOfferingList.Contains(unitOffcering))
                    {
                        return entry;
                    }
                }
            }

            return null;

        }

        private async Task<Unit> FindUnit(UnitOffering unitOffering)
        {

            foreach (var entry in await _db.Unit.ToListAsync())
            {

                await _db.Entry(entry)
                    .Collection(unitOffering => unitOffering.UnitOfferingList)
                    .LoadAsync();

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
            Console.WriteLine("made it to generate activities");

            var discipline = _db.Discipline.FirstOrDefault();

            List<TeachingActivity> teachingActivities = new List<TeachingActivity>();

            //some code to create activities depending on flags 


            //function params:     UnitCode           TeachingPeriod            activity name             sessions per week         hours per session

            if (_unitOffering.WOCT_Flag) //whole cohort shiznick
            {

                


                string title = "Whole of Cohort Teaching - First";

                bool isTransferable = true;

                int activityQTY = _teachingPattern.WOCT_SessionsPerWeekFIRST;

                double ActivityHrs = _teachingPattern.WOCT_HrsPerSessionFIRST;

                int ActivityTotal = (int)(activityQTY * ActivityHrs);

                var activity = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                    title, activityQTY * 12, ActivityHrs);

                teachingActivities.Add(activity);

                if(_teachingPattern.WOCT_SessionsPerWeekREPEAT>0)
                {

                    string title2 = "Whole of Cohort Teaching - Repeat";

                    bool isTransferable2 = true;

                    int activityQTY2 = _teachingPattern.WOCT_SessionsPerWeekREPEAT;

                    double ActivityHrs2 = _teachingPattern.WOCT_HrsPerSessionREPEAT;

                    int ActivityTotal2 = (int)(activityQTY * ActivityHrs);

                    var activity2 = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                        title, activityQTY * 12, ActivityHrs);

                    teachingActivities.Add(activity);

                }

            }
            if (_unitOffering.SGT_Flag) //small group teaching shiznick
            {

                string title2 = "Small Group Teaching";

                bool isTransferable2 = true;

                int activityMultiple = RoundUp(_teachingPattern.TotalEnrolments - _teachingPattern.ExternalEnrolments) / (_teachingPattern.SGT_ClassSize);

                int activityQTY2 = _teachingPattern.SGT_SessionsPerWeek;

                double ActivityHrs2 = _teachingPattern.SGT_HrsPerSession;

                int ActivityTotal2 = (int)(activityQTY2 * ActivityHrs2 * activityMultiple);

                var activity2 = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                    title2, activityQTY2 * 12, ActivityHrs2);

                teachingActivities.Add(activity2);

                //--------------------//

                string title = "Small Group Marking";

                bool isTransferable = true;

                int activityQTY = _teachingPattern.SGT_ClassSize;

                int activityMultipl = RoundUp(_teachingPattern.TotalEnrolments - _teachingPattern.ExternalEnrolments) / (_teachingPattern.SGT_ClassSize);

                double ActivityHrs = discipline.SGT_MarkingHrsPS;

                int ActivityTotal = (int)(activityMultipl * activityQTY * ActivityHrs);

                var activity = CreateTransferable(_unitOffering.UnitCode, _unitOffering.TeachingPeriod,
                    title, activityQTY, ActivityHrs);

                teachingActivities.Add(activity);

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

            AutomaticallyAssignActivities(_unitCoordinator, _unitOffering, teachingActivities);



        }

        private TeachingActivity CreateTransferable(string unitCode, string teachingPeriod, string activityName, int activityQuantity, double ActivityHours)
        {

            Console.WriteLine("made it to create transferable");

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

        private void AutomaticallyAssignActivities(AcademicStaff _unitCoordinator, UnitOffering _unitOffering, List<TeachingActivity> _teachingActivities)
        {


            Console.WriteLine("made automatically assign activities");

            _unitCoordinator.TeachingActivityList.AddRange(_teachingActivities);
            _unitOffering.TeachingActivityList.AddRange(_teachingActivities);

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
                    return list;
                }
            }
            return null;


        }
    }
}
