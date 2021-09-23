using System;
using System.Threading.Tasks;
using MAWS.Models;
using MAWS.Services;
using MAWS.Services.UploadData;
using Xunit;

namespace MAWS.Tests
{
    public class AcademicStaffTests : TestBase
    {
        public AcademicStaff staff;
        UploadAcademicStaff uploadStaff;
        QueryAcademicStaff queryStaff;


        public AcademicStaffTests()
        {
            staff = new AcademicStaff();
            //uploadStaff = new UploadAcademicStaff(context);
        }

        private void CreateTestStaff()
        {
            staff.AcademicStaffID = "Staff123";
            staff.FirstName = "Kavi";
            staff.Surname = "Chapagain";
            staff.EmployeeType = "CON";
            staff.Area = "IT";
            staff.ClassCode = "ACLEA";
            staff.ClassName = "Assoc Lecturer";
            staff.FTBaseHrs = 1725;
            staff.WorkFraction = 1;
            staff.EmployeeStatus = "Fixed Term";
            staff.ContractExpiryDate = "31/12/2020";
            staff.WorkMax_Pc = 0.8;
            staff.TeachingMax_Pc = 1.15;
        }

        [Fact]
        public async Task ShouldAddAndGetAcademicStaff()
        {
            using var context = await GetDbContextAsync();
            CreateTestStaff();

            uploadStaff = new UploadAcademicStaff(context);
            queryStaff = new QueryAcademicStaff(context);

            //add
            await uploadStaff.AddAsync(staff);

            //get
            var data = await queryStaff.GetStaffListAsync();

            Assert.Single(data);
            Assert.Contains(data, d => d.AcademicStaffID == staff.AcademicStaffID);
            Assert.Contains(data, d => d.FirstName == staff.FirstName);
            Assert.Contains(data, d => d.Surname == staff.Surname);
            Assert.Contains(data, d => d.EmployeeType == staff.EmployeeType);
            Assert.Contains(data, d => d.Area == staff.Area);
            Assert.Contains(data, d => d.ClassCode == staff.ClassCode);
            Assert.Contains(data, d => d.ClassName == staff.ClassName);
            Assert.Contains(data, d => d.FTBaseHrs == staff.FTBaseHrs);
            Assert.Contains(data, d => d.WorkFraction == staff.WorkFraction);
            Assert.Contains(data, d => d.EmployeeStatus == staff.EmployeeStatus);
            Assert.Contains(data, d => d.ContractExpiryDate == staff.ContractExpiryDate);
            Assert.Contains(data, d => d.WorkMax_Pc == staff.WorkMax_Pc);
            Assert.Contains(data, d => d.TeachingMax_Pc == staff.TeachingMax_Pc);
        }

    }
}
