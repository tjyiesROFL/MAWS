using MAWS.Models;
using MAWS.IntermediateData;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAWS.Services
{
    public class QueryWorkload
    {
        private readonly ApplicationDbContext _db;

        public QueryWorkload(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<AcademicStaff> LoadAcademicStaff(AcademicStaff entry)
        {

            _db.Attach(entry);

            try 
            {

                await _db.Entry(entry)
                    .Collection(_staff => _staff.ReasearchList)
                    .LoadAsync();
                await _db.Entry(entry)
                    .Collection(_staff => _staff.ServiceList)
                    .LoadAsync();
                await _db.Entry(entry)
                    .Collection(_staff => _staff.SupervisionList)
                    .LoadAsync();
                await _db.Entry(entry)
                    .Collection(_staff => _staff.MiscTeachingActivityList)
                    .LoadAsync();
                await _db.Entry(entry)
                    .Collection(_staff => _staff.TeachingActivityAssignmentList)
                    .LoadAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString(),"Error");
            }
            
            return entry;
        }

        public async Task<TotalWorkload> GetStaffTotalWorkload(AcademicStaff staff)
        {
            return CalculateTotalWorkload(await LoadAcademicStaff(staff));
        }

        public async Task<List<TotalWorkload>> GetStaffTotalWorkload(List<AcademicStaff> staff)
        {

            List<TotalWorkload> workloads = new List<TotalWorkload>();

            foreach (var entry in staff)
            {

                workloads.Add(CalculateTotalWorkload(await LoadAcademicStaff(entry)));

            }

            return workloads;
        }

        public TotalWorkload CalculateTotalWorkload(AcademicStaff staff)
        {

            TotalWorkload tempWorkload = new TotalWorkload(staff);


            if (staff.ReasearchList != null)
            {
                if(staff.ReasearchList.LastOrDefault()!=null)
                {
                    if(staff.ReasearchList.Last().Percentage.HasValue)
                    {

                        tempWorkload.ResearchPercentage = staff.ReasearchList.Last().Percentage.Value;
                        tempWorkload.ResearchHours = tempWorkload.ResearchPercentage * staff.WorkHrs;
                        tempWorkload.TotalHours = tempWorkload.ResearchHours;

                    }
                }
            }

            if(staff.ServiceList != null)
            {
                foreach(var service in staff.ServiceList)
                {

                    tempWorkload.ServiceHours += service.Hours;
                    tempWorkload.TotalHours += service.Hours;

                }
                tempWorkload.ServicePercentage = tempWorkload.ServiceHours / staff.WorkHrs;
            }

            if (staff.SupervisionList != null)
            {
                foreach (var supervision in staff.SupervisionList)
                {

                    tempWorkload.SupervisionHours += supervision.Hours;
                    tempWorkload.TotalHours += supervision.Hours;
                    

                }
                tempWorkload.SupervisionPercentage = tempWorkload.SupervisionHours / staff.WorkHrs;
            }

            if (staff.MiscTeachingActivityList != null)
            {
                foreach (var miscTeaching in staff.MiscTeachingActivityList)
                {

                    tempWorkload.MiscTeachingHours += miscTeaching.Hours;
                    tempWorkload.TotalHours += miscTeaching.Hours;
                   

                }
                tempWorkload.MiscTeachingPercentage = tempWorkload.MiscTeachingHours / staff.WorkHrs;
            }

            if (staff.TeachingActivityAssignmentList != null)
            {
                foreach (var teaching in staff.TeachingActivityAssignmentList)
                {

                    tempWorkload.TeachingHours += teaching.WorkloadHrs;
                    tempWorkload.TotalHours += teaching.WorkloadHrs;

                }
                tempWorkload.TeachingPercentage = tempWorkload.TeachingHours / staff.WorkHrs;
            }

            return tempWorkload;

        }
    }
}