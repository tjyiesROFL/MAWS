using MAWS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System;

namespace MAWS.Services
{
    public class QueryUsers
    {
        private readonly ApplicationDbContext _db;

        public QueryUsers(ApplicationDbContext db)
        {
            _db = db;
        }





        //-------------------------------------Temporary Placement for functions below to maintain Manage Users page -------------------------------------//
        public void AddAcademicStaffApplicationUser(ApplicationUser appUser)
        {

            AspNetUserAcademicStaff newRelation = new AspNetUserAcademicStaff
            {
                Id = appUser.Id,
                StaffID = appUser.AcademicStaffID
            };

            _db.AspNetUserAcademicStaff.Add(newRelation);

            try
            {
                _db.SaveChangesAsync();
                Console.WriteLine("[App User - Academic staff] Save was Successful");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);

            }
        }

        public void UpdateAcademicStaffApplicationUser(ApplicationUser appUser)
        {

            AspNetUserAcademicStaff existingRelation = _db.AspNetUserAcademicStaff.Where(b => b.Id == appUser.Id).FirstOrDefault();

            existingRelation.StaffID = appUser.AcademicStaffID;

            try
            {
                _db.SaveChangesAsync();
                Console.WriteLine("[App User - Academic staff] Save was Successful");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);

            }
        }
    }
}
