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
    public class UploadSupervision : IUploadData
    {
        public string ClassName => nameof(UploadSupervision);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Supervision, string>> _supervisionTupleList = new List<Tuple<Supervision, string>>();


        public UploadSupervision(ApplicationDbContext dbContext)
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
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = ReadFieldsFromCsv();
                        if(IsSupervisionValid(record.Item1))
                        {
                            _supervisionTupleList.Add(record);
                        }
                    }
                }
            }
            await AddSupervisionListAsync();
        }

        private bool IsSupervisionValid(Supervision _supervision)
        {

            //if (!_db.Supervision.Any(o => o.SupervisionID == record.SupervisionID)) { supervisionList.Add(record); } 

            if (_supervision.Year.ToString().Length > 4) { return false; }
            if (_supervision.Hours.ToString().Length > 7) { return false; }
            //if (_supervision.Type.Length > 3) { return false; }
            //if (_supervision.Comments.Length > 255) { return false; }
            //if (_supervision.IS_CURRENT) { return false; }

            return true;
        }


        private Tuple<Supervision, string> ReadFieldsFromCsv()
        {

            Supervision supervision = new Supervision();
            try
            {
                var staffID = csv.GetField("StaffID");
                supervision.Year = int.Parse(csv.GetField("Year"));
                supervision.IS_CURRENT = bool.Parse(csv.GetField("IS_CURRENT"));
                supervision.Hours = double.Parse(csv.GetField("Hrs"));
                supervision.Type = csv.GetField("Type");
                supervision.Comments = csv.GetField("Comments");
                return new Tuple<Supervision, string>(supervision, staffID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddSupervisionListAsync()
        {
            foreach (var record in _supervisionTupleList)
            {
                AcademicStaff academicStaff = await _db.AcademicStaff.Where(b => b.AcademicStaffID == record.Item2).FirstOrDefaultAsync();
                if(academicStaff != null)
                {
                    if (academicStaff.SupervisionList == null)
                    {
                        academicStaff.SupervisionList = new List<Supervision>();
                    }
                    academicStaff.SupervisionList.Add(record.Item1);
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
