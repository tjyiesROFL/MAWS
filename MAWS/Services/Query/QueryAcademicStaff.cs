using MAWS.Models;
using MAWS.IntermediateData;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAWS.Services
{
    public class QueryAcademicStaff
    {
        private readonly ApplicationDbContext _db;

        public QueryAcademicStaff(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<AcademicStaff>> GetStaffListAsync()
        {

            return await _db.AcademicStaff.ToListAsync();

        }

        public List<string> GetStaffIDList()
        {
            List<string> buffer = new List<string>();
            var temp =  _db.AcademicStaff.ToList();
            foreach(var entry in temp)
            {
                buffer.Add(entry.AcademicStaffID);
            }
            return buffer;
        }

        public AcademicStaff GetStaff(string StaffID)
        {
            return _db.AcademicStaff.Find(StaffID);
        }

        public List<AcademicStaff> GetSessionalStaffList()
        {
            List<AcademicStaff> staffList = _db.AcademicStaff.ToList();

            List<AcademicStaff> tempList = new List<AcademicStaff>();

            foreach (var entry in staffList)
            {
                if (entry.EmployeeStatus == "Sessional")
                {
                    tempList.Add(entry);
                }
            }
            return tempList;
        }

        public List<string> GetStaffIDListNoAccount()// finding all staff that do not yet have MAWS accounts
        {

            //not sure if this is the most efficient way of searching

            List<AcademicStaff> _staffList = _db.AcademicStaff.ToList();
            List<AspNetUserAcademicStaff> _accountList = _db.AspNetUserAcademicStaff.ToList();

            List<string> staffIDList = new List<string>();
            List<string> userStaffIDList = new List<string>();

            foreach (var record in _staffList)
            {
                staffIDList.Add(record.AcademicStaffID);
            }
            foreach (var record in _accountList)
            {
                userStaffIDList.Add(record.StaffID);
            }

            return staffIDList.Except(userStaffIDList).ToList();

        }
    }
}
