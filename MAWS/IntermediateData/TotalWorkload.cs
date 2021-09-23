using System;
using MAWS.Models;

namespace MAWS.IntermediateData
{
    /// <summary>
    /// 
    /// Class contaimns total workload data
    /// 
    /// 
    /// Jack
    /// 
    /// </summary>
    
    public class TotalWorkload
    {

        private AcademicStaff _academicStaff;

        public double TeachingHours;

        public double ResearchHours;

        public double ServiceHours;

        public double SupervisionHours;

        public double MiscTeachingHours;

        public double TeachingPercentage;

        public double ResearchPercentage;

        public double ServicePercentage;

        public double SupervisionPercentage;

        public double MiscTeachingPercentage;

        public double TotalHours;

        public double TotalPercentage;

        public TotalWorkload()
        {

            _academicStaff = null;
            TotalHours = 0;
            TotalPercentage = 0;
            TeachingHours = 0;
            ResearchHours = 0;
            ServiceHours = 0;
            SupervisionHours = 0;
            MiscTeachingHours = 0;
            TeachingPercentage = 0;
            ResearchPercentage = 0;
            ServicePercentage = 0;
            SupervisionPercentage = 0;
            MiscTeachingPercentage = 0;

        }

        public TotalWorkload(AcademicStaff staff)
        {

            _academicStaff = staff;
            TotalHours = 0;
            TotalPercentage = 0;
            TeachingHours = 0;
            ResearchHours = 0;
            ServiceHours = 0;
            SupervisionHours = 0;
            MiscTeachingHours = 0;
            TeachingPercentage = 0;
            ResearchPercentage = 0;
            ServicePercentage = 0;
            SupervisionPercentage = 0;
            MiscTeachingPercentage = 0;

        }

        public void SetAcademicStaff(AcademicStaff staff)
        {
            _academicStaff = staff;
        }

        public string GetStaffFirstName()
        {
            return _academicStaff.FirstName;
        }

        public string GetStaffSurname()
        {
            return _academicStaff.Surname;
        }
    }
}