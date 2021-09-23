using MAWS.Models;
using System;

namespace MAWS.IntermediateData
{

    
    public class IntermediateService
    {

        public int ServiceID { get; set; }
        public string AcademicStaffID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Year { get; set; }
        public double Hours { get; set; }
        public string Type { get; set; }
        public string Comments { get; set; }
        public bool IS_CURRENT { get; set; }

        public IntermediateService()
        {

        }
    }
}