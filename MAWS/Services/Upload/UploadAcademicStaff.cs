using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using MAWS.Models;
using MAWS.Services;
//        JsRuntime.InvokeAsync<object>("DataTablesRemove", "#compare-academic-staff-table");
namespace MAWS.Services.UploadData
{
    public class UploadAcademicStaff : IUploadData
    {
        public string ClassName  => nameof(UploadAcademicStaff);

        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<AcademicStaff> _validStaffList = new List<AcademicStaff>();

        public UploadAcademicStaff(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task Upload(MemoryStream ms)
        {
            //var validStaffList = new List<AcademicStaff>();
            var invalidStaffList = new List<AcademicStaff>();

            using (var reader = new System.IO.StreamReader(ms))
            {
                ms.Position = 0;
                using (csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var staff = ReadFieldsFromCsv();
                        if (IsValid(staff))
                        {
                            _validStaffList.Add(staff);
                        }
                        else
                        {
                            invalidStaffList.Add(staff);
                        }
                    }
                }
            }

            await AddAcademicStaffListAsync();
        }

        private bool IsValid(AcademicStaff staff)
        {
            if (staff.AcademicStaffID.Length > 8) { return false; }
            if (staff.FirstName.Length > 255) { return false; }
            if (staff.Surname.Length > 255) { return false; }
            if (staff.EmployeeType.Length > 3) { return false; }
            if (staff.Area.Length > 3) { return false; }
            if (staff.ClassCode.Length > 6) { return false; }
            if (staff.ClassName.Length > 255) { return false; }
            if (staff.FTBaseHrs.ToString().Length > 4) { return false; }
            if (staff.WorkFraction.ToString().Length > 4) { return false; }
            if (staff.EmployeeStatus.Length > 20) { return false; }
            if (staff.ContractExpiryDate.Length > 10) { return false; }
            if (staff.WorkMax_Pc.ToString().Length > 4) { return false; }
            if (staff.WorkHrs.ToString().Length > 7) { return false; }
            if (staff.TeachingMax_Pc.ToString().Length > 4) { return false; }

            return true;
        }

        private AcademicStaff ReadFieldsFromCsv()
        {
            AcademicStaff staff = new AcademicStaff();
            try
            {
                staff.AcademicStaffID = csv.GetField("StaffID");
                staff.FirstName = csv.GetField("FirstName");
                staff.Surname = csv.GetField("Surname");
                staff.EmployeeType = csv.GetField("EmployeeType");
                staff.Area = csv.GetField("Area");
                staff.ClassCode = csv.GetField("ClassCode");
                staff.ClassName = csv.GetField("ClassName");
                staff.FTBaseHrs = int.Parse(csv.GetField("FTBaseHrs"));
                staff.WorkFraction = double.Parse(csv.GetField("Work_Fraction"));
                staff.EmployeeStatus = csv.GetField("EmployeeStatus");
                staff.ContractExpiryDate = csv.GetField("ContractExpiryDate");
                staff.WorkMax_Pc = double.Parse(csv.GetField("WorklMax_Pc"));
                staff.TeachingMax_Pc = double.Parse(csv.GetField("TeachingMax_Pc"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
            }
            return staff; 
        }

        private async Task AddAcademicStaffListAsync()
        {
            try
            {
                await _db.AcademicStaff.AddRangeAsync(_validStaffList);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }

        public async Task AddAsync(AcademicStaff academicStaff)
        {
            try
            {
                await _db.AcademicStaff.AddAsync(academicStaff);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}
