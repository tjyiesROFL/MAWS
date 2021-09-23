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
    public class UnitOfferingService : IDataAccessServices<IntermediateUnitOffering>
    {
        public string ClassName => nameof(UnitOfferingService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<UnitOffering, string, string>> _unitOfferingOfferingTupleList = new List<Tuple<UnitOffering, string, string>>();


        public UnitOfferingService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }


        public async Task<List<IntermediateUnitOffering>> GetListAsync()
        {
            List<IntermediateUnitOffering> intrUnitOfferingList = new List<IntermediateUnitOffering>();

            foreach (var unitOffering in await _db.UnitOffering.ToListAsync())
            {
                if (unitOffering != null)
                {

                    var intrUnit = new IntermediateUnitOffering();

                    intrUnit.UnitOfferingID = unitOffering.UnitOfferingID;
                    intrUnit.UnitCode = unitOffering.UnitCode;
                    intrUnit.Year = unitOffering.Year;
                    intrUnit.TeachingPeriod = unitOffering.TeachingPeriod;
                    intrUnit.Location = unitOffering.Location;
                    intrUnit.Mode = unitOffering.Mode;
                    intrUnit.OUAE_Flag = unitOffering.OUAE_Flag;
                    intrUnit.WOCT_Flag = unitOffering.WOCT_Flag;
                    intrUnit.SGT_Flag = unitOffering.SGT_Flag;
                    intrUnit.OfferingType = unitOffering.OfferingType;
                    intrUnit.ActiveFlag = unitOffering.ActiveFlag;

                    intrUnitOfferingList.Add(intrUnit);


                }
            }
            return intrUnitOfferingList;
        }

        // GetAsync if needed?

        public async Task<bool> AddAsync(IntermediateUnitOffering intrUnit)
        {

            if (_db.UnitOffering.Find(intrUnit.UnitOfferingID) != null)
            {

                var unitOffering = await _db.UnitOffering
                    .Where(r => r.UnitCode == intrUnit.UnitCode)
                    .FirstOrDefaultAsync();

                unitOffering.UnitOfferingID = intrUnit.UnitOfferingID;
                unitOffering.UnitCode = intrUnit.UnitCode;
                unitOffering.Year = intrUnit.Year;
                unitOffering.TeachingPeriod = intrUnit.TeachingPeriod;
                unitOffering.Location = intrUnit.Location;
                unitOffering.Mode = intrUnit.Mode;
                unitOffering.OUAE_Flag = intrUnit.OUAE_Flag;
                unitOffering.WOCT_Flag = intrUnit.WOCT_Flag;
                unitOffering.SGT_Flag = intrUnit.SGT_Flag;
                unitOffering.OfferingType = intrUnit.OfferingType;
                unitOffering.ActiveFlag = intrUnit.ActiveFlag;

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                var unit = await _db.Unit
                    .Where(r => r.UnitCode == intrUnit.UnitCode)
                    .FirstOrDefaultAsync();

                var unitOffering = new UnitOffering();

                unitOffering.UnitOfferingID = intrUnit.UnitOfferingID;
                unitOffering.UnitCode = intrUnit.UnitCode;
                unitOffering.Year = intrUnit.Year;
                unitOffering.TeachingPeriod = intrUnit.TeachingPeriod;
                unitOffering.Location = intrUnit.Location;
                unitOffering.Mode = intrUnit.Mode;
                unitOffering.OUAE_Flag = intrUnit.OUAE_Flag;
                unitOffering.WOCT_Flag = intrUnit.WOCT_Flag;
                unitOffering.SGT_Flag = intrUnit.SGT_Flag;
                unitOffering.OfferingType = intrUnit.OfferingType;
                unitOffering.ActiveFlag = intrUnit.ActiveFlag;

                unit.UnitOfferingList.Add(unitOffering);
                return true;
            }
        }

        public async Task RemoveAsync(IntermediateUnitOffering intermediateUnit)
        {
            var unitOffering = await _db.Unit
                .Where(r => r.UnitCode == intermediateUnit.UnitCode)
                .FirstOrDefaultAsync();

            _db.Unit.Remove(unitOffering);

            await _db.SaveChangesAsync();
        }

        //should move in academicstaffservice
        public async Task<List<string>> GetIdListAsync()
        {
            return await _db.AcademicStaff.Select(a => a.AcademicStaffID).ToListAsync();
        }

        public async Task UploadAsync(MemoryStream ms)
        {
            // using (var scope = _serviceScopeFactory.CreateScope())
            // {
            using (var reader = new System.IO.StreamReader(ms))
            {
                ms.Position = 0;
                using (csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    while (await csv.ReadAsync())
                    {
                        var record = ReadFieldsFromCsv();
                        if (IsUnitOfferingValid(record.Item1))
                        {
                            _unitOfferingOfferingTupleList.Add(record);
                        }
                    }
                }
            }
            await AddUnitOfferingListAsync();
        }

        private bool IsUnitOfferingValid(UnitOffering _unitOfferingOffering)
        {

            //if (!_db.UnitOffering.Any(o => o.UnitOfferingID == record.UnitOfferingID)) { unitOfferingOfferingList.Add(record); }
            //else {Console.WriteLine("Error: " + record.UnitOfferingID + " <- Duplicates not allowed."); }; Find(record.UnitCode)

            return true;
        }

        private Tuple<UnitOffering, string, string> ReadFieldsFromCsv()
        {
            UnitOffering unitOfferingOffering = new UnitOffering();
            try
            {
                var unitOfferingCode = csv.GetField("UnitCode");
                var unitOfferingCoord = csv.GetField("UnitCoordinator");
                unitOfferingOffering.UnitOfferingID = csv.GetField("UnitCode") + csv.GetField("Year") + csv.GetField("TeachingPeriod");
                unitOfferingOffering.UnitCode = csv.GetField("UnitCode");
                unitOfferingOffering.Year = int.Parse(csv.GetField("Year"));
                unitOfferingOffering.TeachingPeriod = csv.GetField("TeachingPeriod");
                unitOfferingOffering.Location = csv.GetField("Location");
                unitOfferingOffering.OfferingType = csv.GetField("OfferingType");
                unitOfferingOffering.Mode = csv.GetField("Mode");
                unitOfferingOffering.ActiveFlag = bool.Parse(csv.GetField("ActiveFlag"));
                return new Tuple<UnitOffering, string, string>(unitOfferingOffering, unitOfferingCode, unitOfferingCoord);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddUnitOfferingListAsync()
        {
            foreach (var record in _unitOfferingOfferingTupleList)
            {
                Unit unitOffering = await _db.Unit.Where(b => b.UnitCode == record.Item2).FirstOrDefaultAsync();
                AcademicStaff unitOfferingCoord = await _db.AcademicStaff.Where(b => b.AcademicStaffID == record.Item3).FirstOrDefaultAsync();

                if (unitOffering != null)
                {
                    if (unitOffering.UnitOfferingList == null)
                    {
                        unitOffering.UnitOfferingList = new List<UnitOffering>();
                    }

                    unitOffering.UnitOfferingList.Add(record.Item1);
                }

                if (unitOfferingCoord != null)
                {
                    if (unitOfferingCoord.UnitOfferingList == null)
                    {
                        unitOfferingCoord.UnitOfferingList = new List<UnitOffering>();
                    }

                    unitOfferingCoord.UnitOfferingList.Add(record.Item1);
                }
            }

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
                //Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
