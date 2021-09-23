using MAWS.Models;
using MAWS.IntermediateData;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAWS.Services
{
    public class QueryResearch
    {
        private readonly ApplicationDbContext _db;

        public QueryResearch(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<AcademicStaff> GetStaffResearchList()
        {
            List<AcademicStaff> tempStaffResearchList = _db.AcademicStaff.ToList();
            List<AcademicStaff> staffResearchList = new List<AcademicStaff>();

            foreach (var entry in tempStaffResearchList)
            {
                _db.Entry(entry)
                    .Collection(u => u.ReasearchList)
                    .Load();
                if (entry.ReasearchList != null)
                {
                    staffResearchList.Add(entry);
                }
            }

            return staffResearchList;

        }
    }
}