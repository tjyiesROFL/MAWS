using MAWS.Models;
using System;

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
    
    public class IntermediateAcademicStaff
    {

        public string StaffID { get; set; }
        public string AcademicStaffID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmployeeType { get; set; }
        public string Area { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public int FTBaseHrs { get; set; }
        public double WorkFraction { get; set; }
        public string EmployeeStatus { get; set; }
        public string ContractExpiryDate { get; set; }
        public double WorkMax_Pc { get; set; }
        public double WorkHrs { get; set; }
        public double TeachingMax_Pc { get; set; }

        public IntermediateAcademicStaff()
        {

        }
    }
}   