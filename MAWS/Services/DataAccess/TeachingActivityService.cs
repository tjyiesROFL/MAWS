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
    public class TeachingPatternService : IDataAccessServices<IntermediateTeachingPattern>
    {
        public string ClassName => nameof(TeachingPatternService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<TeachingPattern, string>> _teachingPatternTupleList = new List<Tuple<TeachingPattern, string>>();


        public TeachingPatternService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }


        public async Task<List<IntermediateTeachingPattern>> GetListAsync()
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
            var unitCoordinator = findAcademicStaff(unitOffering);

            await _db.Entry(unitOffering)
                .Collection(unitOffering => unitOffering.TeachingPatternList)
                .LoadAsync();
            await _db.Entry(unitCoordinator)
                .Collection(unitOffering => unitOffering.TeachingPatternList)
                .LoadAsync();

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

        private AcademicStaff findAcademicStaff(UnitOffering unitOffcering)
        {

            foreach(var entry in _db.AcademicStaff.ToList())
            {

                _db.Entry(entry)
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

        /// this function should create required activities and automatically assign all the unit coordinator
        private void GenerateActivities(AcademicStaff unitCoordinator, TeachingPattern teachingPattern, UnitOffering unitOffering, Unit unit)
        {


            //some code to create activities depending on flags 


            

            


        }

        private TeachingActivity CreateLecutreOrTutorial(string unitCode, int year, string teachingPeriod, bool transferable, string activityName, int activityQuantity, int ActivityHours, bool activeFlag)
        {
            TeachingActivity teachingActivity = new TeachingActivity
            {
                UnitCode = unitCode,
                Year = year,
                TeachingPeriod = teachingPeriod,
                ActivityWeeks = 12,
                Transferable = transferable,
                Activity = activityName,
                ActivityQty = activityQuantity,
                ActivityHrs = (int)ActivityHours,
                ActiveFlag = activeFlag
            };
            return teachingActivity;
        }
    }
}
