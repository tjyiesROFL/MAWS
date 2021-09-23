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
    public class AcademicServiceService : IDataAccessServices<IntermediateService> 
    {
        public string ClassName => nameof(AcademicServiceService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Service, string>> _serviceTupleList = new List<Tuple<Service, string>>();


        public AcademicServiceService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        
        public async Task<List<IntermediateService>> GetListAsync()
        {
            List<IntermediateService> intrServiceList = new List<IntermediateService>();
            var res = await _db.AcademicStaff.Include(a => a.ServiceList).ToListAsync();

            foreach (var staff in res)
            {
                if (staff != null)
                {
                    foreach (var entry in staff.ServiceList)
                    {
                        if (staff != null)
                        {
                            var intrService = new IntermediateService();

                            intrService.ServiceID = entry.ServiceID;
                            intrService.AcademicStaffID = staff.AcademicStaffID;
                            intrService.FirstName = staff.FirstName;
                            intrService.Surname = staff.Surname;
                            intrService.Year = entry.Year;
                            intrService.Hours = entry.Hours;
                            intrService.Type = entry.Type;
                            intrService.Comments = entry.Comments;

                            intrServiceList.Add(intrService);
                        }
                    }
                }
            }
            return intrServiceList; 
        }

        // GetAsync if needed?

        public async Task<bool> AddAsync(IntermediateService intermediateService)
        {
            var service = await _db.Service
                .Where(r => r.ServiceID == intermediateService.ServiceID)
                .FirstOrDefaultAsync();
            
            if (service != null)
            {
                service.Year = intermediateService.Year;
                service.Hours = intermediateService.Hours;
                service.Comments = intermediateService.Comments;
                service.Type = intermediateService.Type;
                service.IS_CURRENT = intermediateService.IS_CURRENT;

                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task RemoveAsync(IntermediateService intermediateService)
        {
            var service = await _db.Service
                .Where(r => r.ServiceID == intermediateService.ServiceID)
                .FirstOrDefaultAsync();

            _db.Service.Remove(service);

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
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    while (await csv.ReadAsync())
                    {
                        var record = ReadFieldsFromCsv();
                        if (IsServiceValid(record.Item1))
                        {
                            _serviceTupleList.Add(record);
                        }
                    }
                }
            }
            await AddServiceListAsync();
        }

        private bool IsServiceValid(Service _service)
        {

            if (_service.Year.ToString().Length > 4) { return false; }
            if (_service.Hours.ToString().Length > 7) { return false; }
            if (_service.Type.Length > 32) { return false; }

            //if (_service.Comments.Length > 255) { return false; } //null check or default value required

            //if (_service.IS_CURRENT) { return false; }


            //if (!_db.Service.Any(o => o.ServiceID == record.ServiceID)){ serviceList.Add(record); } need some logic to make sure the EXACT same entries can't be entered
            //else { Console.WriteLine("entry " + record.ServiceID + " already exists"); }

            return true;
        }

        private Tuple<Service, string> ReadFieldsFromCsv()
        {

            Service service = new Service();
            try
            {
                var staffID = csv.GetField("StaffID");
                service.Year = int.Parse(csv.GetField("Year"));
                service.IS_CURRENT = bool.Parse(csv.GetField("IS_CURRENT"));
                service.Type = csv.GetField("Type");
                service.Hours = double.Parse(csv.GetField("Hrs"));
                return new Tuple<Service, string>(service, staffID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddServiceListAsync()
        {

            foreach (var record in _serviceTupleList)
            {
                AcademicStaff academicStaff = await _db.AcademicStaff.Where(b => b.AcademicStaffID == record.Item2).FirstOrDefaultAsync();
                if (academicStaff != null)
                {
                    if (academicStaff.ServiceList == null)
                    {
                        academicStaff.ServiceList = new List<Service>();
                    }
                    academicStaff.ServiceList.Add(record.Item1);
                }

            }

            try { await _db.SaveChangesAsync(); }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
