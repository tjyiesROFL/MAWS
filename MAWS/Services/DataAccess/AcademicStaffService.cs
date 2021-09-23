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
    public class AcademicStaffService : IDataAccessServices<IntermediateAcademicStaff> 
    {
        public string ClassName => nameof(AcademicStaffService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<AcademicStaff> _validStaffList = new List<AcademicStaff>();


        public AcademicStaffService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        
        public async Task<List<IntermediateAcademicStaff>> GetListAsync()
        {
            List<IntermediateAcademicStaff> intrAcademicStaffList = new List<IntermediateAcademicStaff>();
            var res = await _db.AcademicStaff.ToListAsync();

            foreach (var staff in res)
            {

                if (staff != null)
                {
                    var intrAcademicStaff = new IntermediateAcademicStaff();

                    intrAcademicStaff.AcademicStaffID = staff.AcademicStaffID;
                    intrAcademicStaff.FirstName = staff.FirstName;
                    intrAcademicStaff.Surname = staff.Surname;
                    intrAcademicStaff.EmployeeType = staff.EmployeeType;
                    intrAcademicStaff.Area = staff.Area;
                    intrAcademicStaff.ClassCode = staff.ClassCode;
                    intrAcademicStaff.ClassName = staff.ClassName;
                    intrAcademicStaff.FTBaseHrs = staff.FTBaseHrs;
                    intrAcademicStaff.WorkFraction = staff.WorkFraction;
                    intrAcademicStaff.EmployeeStatus = staff.EmployeeStatus;
                    intrAcademicStaff.ContractExpiryDate = staff.ContractExpiryDate;
                    intrAcademicStaff.WorkMax_Pc = staff.WorkMax_Pc;
                    intrAcademicStaff.WorkHrs = staff.WorkHrs;
                    intrAcademicStaff.TeachingMax_Pc = staff.TeachingMax_Pc;

                    intrAcademicStaffList.Add(intrAcademicStaff);
                }

            }
            return intrAcademicStaffList; 
        }

        // GetAsync if needed?

        public async Task<bool> AddAsync(IntermediateAcademicStaff intermediateAcademicStaff)
        {
            var staff = await _db.AcademicStaff
                .Where(r => r.AcademicStaffID == intermediateAcademicStaff.AcademicStaffID)
                .FirstOrDefaultAsync();
            
            if (staff != null)
            {
                staff.AcademicStaffID = intermediateAcademicStaff.AcademicStaffID;
                staff.FirstName = intermediateAcademicStaff.FirstName;
                staff.Surname = intermediateAcademicStaff.Surname;
                staff.EmployeeType = intermediateAcademicStaff.EmployeeType;
                staff.Area = intermediateAcademicStaff.Area;
                staff.ClassCode = intermediateAcademicStaff.ClassCode;
                staff.ClassName = intermediateAcademicStaff.ClassName;
                staff.FTBaseHrs = intermediateAcademicStaff.FTBaseHrs;
                staff.WorkFraction = intermediateAcademicStaff.WorkFraction;
                staff.EmployeeStatus = intermediateAcademicStaff.EmployeeStatus;
                staff.ContractExpiryDate = intermediateAcademicStaff.ContractExpiryDate;
                staff.WorkMax_Pc = intermediateAcademicStaff.WorkMax_Pc;
                staff.WorkHrs = intermediateAcademicStaff.WorkHrs;
                staff.TeachingMax_Pc = intermediateAcademicStaff.TeachingMax_Pc;

                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task RemoveAsync(IntermediateAcademicStaff intermediateAcademicStaff)
        {
            var research = await _db.AcademicStaff
                .Where(r => r.AcademicStaffID == intermediateAcademicStaff.AcademicStaffID)
                .FirstOrDefaultAsync();

            _db.AcademicStaff.Remove(research);

            await _db.SaveChangesAsync();
        }

        //should move in academicstaffservice
        public async Task<List<string>> GetIdListAsync()
        {
            return await _db.AcademicStaff.Select(a => a.AcademicStaffID).ToListAsync();
        }

        public async Task UploadAsync(MemoryStream ms)
        {

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
