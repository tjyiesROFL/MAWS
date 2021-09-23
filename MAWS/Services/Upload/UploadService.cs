using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using MAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace MAWS.Services.UploadData
{
    public class UploadService : IUploadData
    {
        public string ClassName => nameof(UploadService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Service, string>> _serviceTupleList = new List<Tuple<Service, string>>();

        public UploadService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task Upload(MemoryStream ms)
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
                        if(IsServiceValid(record.Item1))
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
