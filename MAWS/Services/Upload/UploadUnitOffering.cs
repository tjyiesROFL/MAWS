using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using MAWS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MAWS.Services.UploadData
{
    public class UploadUnitOffering : IUploadData
    {
        public string ClassName => nameof(UploadUnitOffering);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<UnitOffering, string, string>> _unitOfferingTupleList = new List<Tuple<UnitOffering, string, string>>();
        protected readonly IServiceScopeFactory _serviceScopeFactory;

        public UploadUnitOffering(ApplicationDbContext dbContext, IServiceScopeFactory serviceScopeFactory)
        {
            _db = dbContext;
            // _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Upload(MemoryStream ms)
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
                        if(IsUnitOfferingValid(record.Item1))
                        {
                            _unitOfferingTupleList.Add(record);
                        }
                    }
                }
            }
            await AddUnitOfferingListAsync();
        }

        private bool IsUnitOfferingValid(UnitOffering _unitOffering)
        {

            //if (!_db.UnitOffering.Any(o => o.UnitOfferingID == record.UnitOfferingID)) { unitOfferingList.Add(record); }
            //else {Console.WriteLine("Error: " + record.UnitOfferingID + " <- Duplicates not allowed."); }; Find(record.UnitCode)

            return true;
        }

        private Tuple<UnitOffering, string, string> ReadFieldsFromCsv()
        {
            UnitOffering unitOffering = new UnitOffering();
            try
            {
                var unitCode = csv.GetField("UnitCode");
                var unitCoord = csv.GetField("UnitCoordinator");
                unitOffering.UnitOfferingID = csv.GetField("UnitCode") + csv.GetField("Year") + csv.GetField("TeachingPeriod");
                unitOffering.UnitCode = csv.GetField("UnitCode");
                unitOffering.Year = int.Parse(csv.GetField("Year"));
                unitOffering.TeachingPeriod = csv.GetField("TeachingPeriod");
                unitOffering.Location = csv.GetField("Location");
                unitOffering.OfferingType = csv.GetField("OfferingType");
                unitOffering.Mode = csv.GetField("Mode");
                unitOffering.WOCT_Flag = bool.Parse(csv.GetField("WOCT_Flag"));
                unitOffering.SGT_Flag = bool.Parse(csv.GetField("SGT_Flag"));
                unitOffering.OUAE_Flag = bool.Parse(csv.GetField("OUAE_Flag"));
                unitOffering.ActiveFlag = bool.Parse(csv.GetField("ActiveFlag"));
                return new Tuple<UnitOffering, string, string>(unitOffering, unitCode, unitCoord);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddUnitOfferingListAsync()
        {
            foreach (var record in _unitOfferingTupleList)
            {
                Unit unit = await _db.Unit.Where(b => b.UnitCode == record.Item2).FirstOrDefaultAsync();
                AcademicStaff unitCoord = await _db.AcademicStaff.Where(b => b.AcademicStaffID == record.Item3).FirstOrDefaultAsync();

                if (unit != null)
                {
                    if (unit.UnitOfferingList == null)
                    {
                        unit.UnitOfferingList = new List<UnitOffering>();
                    }

                    unit.UnitOfferingList.Add(record.Item1);
                }

                if (unitCoord != null)
                {
                    if (unitCoord.UnitOfferingList == null)
                    {
                        unitCoord.UnitOfferingList = new List<UnitOffering>();
                    }

                    unitCoord.UnitOfferingList.Add(record.Item1);
                }
            }

            try { 
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
