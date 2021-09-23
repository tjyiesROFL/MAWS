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
    public class MiscTeachingActivityService : IDataAccessServices<IntermediateMiscTeachingActivity> 
    {
        public string ClassName => nameof(MiscTeachingActivity);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<MiscTeachingActivity, string, string>> _miscTeachingActivityTupleList = new List<Tuple<MiscTeachingActivity, string, string>>();


        public MiscTeachingActivityService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        
        public async Task<List<IntermediateMiscTeachingActivity>> GetListAsync()
        {
            List<IntermediateMiscTeachingActivity> intrMiscTeachingActivityList = new List<IntermediateMiscTeachingActivity>();
            var res = await _db.AcademicStaff.Include(a => a.MiscTeachingActivityList).ToListAsync();

            foreach (var staff in res)
            {
                if (staff != null)
                {
                    foreach (var entry in staff.MiscTeachingActivityList)
                    {
                        if (staff != null)
                        {
                            var intrMiscTeachingActivity = new IntermediateMiscTeachingActivity();

                            intrMiscTeachingActivity.MiscTeachingActivityID = entry.MiscTeachingActivityID;
                            intrMiscTeachingActivity.AcademicStaffID = staff.AcademicStaffID;
                            intrMiscTeachingActivity.FirstName = staff.FirstName;
                            intrMiscTeachingActivity.Surname = staff.Surname;
                            intrMiscTeachingActivity.Year = entry.Year;
                            intrMiscTeachingActivity.Hours = entry.Hours;
                            intrMiscTeachingActivity.MiscName = entry.MiscName;
                            intrMiscTeachingActivity.Comments = entry.Comments;
                            intrMiscTeachingActivity.UnitCode = entry.UnitCode;
                            intrMiscTeachingActivity.TeachingPeriod = entry.TeachingPeriod;

                            intrMiscTeachingActivityList.Add(intrMiscTeachingActivity);
                        }
                    }
                }
            }
            return intrMiscTeachingActivityList; 
        }

        // GetAsync if needed?

        public async Task<bool> AddAsync(IntermediateMiscTeachingActivity intermediateMiscTeachingActivity)
        {
            var miscTeachingActivity = await _db.MiscTeachingActivity
                .Where(r => r.MiscTeachingActivityID == intermediateMiscTeachingActivity.MiscTeachingActivityID)
                .FirstOrDefaultAsync();
            
            if (miscTeachingActivity != null)
            {

                miscTeachingActivity.Year = intermediateMiscTeachingActivity.Year;
                miscTeachingActivity.UnitCode = intermediateMiscTeachingActivity.UnitCode;
                miscTeachingActivity.TeachingPeriod = intermediateMiscTeachingActivity.TeachingPeriod;
                miscTeachingActivity.MiscName = intermediateMiscTeachingActivity.MiscName;
                miscTeachingActivity.Hours = intermediateMiscTeachingActivity.Hours;
                miscTeachingActivity.Comments = intermediateMiscTeachingActivity.Comments;

                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task RemoveAsync(IntermediateMiscTeachingActivity intermediateMiscTeachingActivity)
        {
            var miscTeachingActivity = await _db.MiscTeachingActivity
                .Where(r => r.MiscTeachingActivityID == intermediateMiscTeachingActivity.MiscTeachingActivityID)
                .FirstOrDefaultAsync();

            _db.MiscTeachingActivity.Remove(miscTeachingActivity);

            await _db.SaveChangesAsync();
        }

        //should move in academicstaffservice
        public async Task<List<string>> GetIdListAsync()
        {
            return await _db.AcademicStaff.Select(a => a.AcademicStaffID).ToListAsync();
        }

        public async Task UploadAsync(MemoryStream ms)
        {
            using (var reader = new System.IO.StreamReader(ms))
            {
                ms.Position = 0;
                using (csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = ReadFieldsFromCsv();
                        if (IsMiscTeachingActivity(record.Item1))
                        {
                            _miscTeachingActivityTupleList.Add(record);
                        }
                    }
                }
            }
            await AddMiscTeachingActivityListAsync();
        }

        private bool IsMiscTeachingActivity(MiscTeachingActivity _miscTeachingActivity)
        {

            if (_miscTeachingActivity.Year.ToString().Length > 4) { return false; }
            if (_miscTeachingActivity.UnitCode.Length > 12) { return false; }
            if (_miscTeachingActivity.TeachingPeriod.Length > 12) { return false; }
            if (_miscTeachingActivity.MiscName.Length > 55) { return false; }
            if (_miscTeachingActivity.Hours.ToString().Length > 7) { return false; }
            //if (_miscTeachingActivity.Comments.Length > 7) { return false; }
            //if (_miscTeachingActivity.IS_CURRENT) { return false; }

            return true;
        }

        private Tuple<MiscTeachingActivity, string, string> ReadFieldsFromCsv()
        {
            MiscTeachingActivity miscTeachingActivity = new MiscTeachingActivity();
            try
            {
                var unitOfferingID = csv.GetField("UnitCode") + csv.GetField("Year") + csv.GetField("TeachingPeriod");
                var unitCoord = csv.GetField("UnitCoordinator");
                miscTeachingActivity.UnitCode = csv.GetField("UnitCode");
                miscTeachingActivity.Year = int.Parse(csv.GetField("Year"));
                miscTeachingActivity.TeachingPeriod = csv.GetField("TeachingPeriod");
                miscTeachingActivity.MiscName = csv.GetField("MiscName");
                miscTeachingActivity.Hours = double.Parse(csv.GetField("Hours"));
                miscTeachingActivity.Comments = csv.GetField("Comments");
                return new Tuple<MiscTeachingActivity, string, string>(miscTeachingActivity, unitOfferingID, unitCoord);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddMiscTeachingActivityListAsync()
        {
            foreach (var record in _miscTeachingActivityTupleList)
            {
                var unitOffering = await _db.UnitOffering.Where(b => b.UnitOfferingID == record.Item2).FirstOrDefaultAsync();
                var unitCoord = await _db.AcademicStaff.Where(b => b.AcademicStaffID == record.Item3).FirstOrDefaultAsync();

                if (unitOffering != null)
                {
                    if (unitOffering.MiscTeachingActivityList == null)
                    {
                        unitOffering.MiscTeachingActivityList = new List<MiscTeachingActivity>();
                    }
                    unitOffering.MiscTeachingActivityList.Add(record.Item1);
                }

                if (unitCoord != null)
                {
                    if (unitCoord.MiscTeachingActivityList == null)
                    {
                        unitCoord.MiscTeachingActivityList = new List<MiscTeachingActivity>();
                    }
                    unitCoord.MiscTeachingActivityList.Add(record.Item1);
                }
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
