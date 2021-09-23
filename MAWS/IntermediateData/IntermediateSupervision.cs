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
    
    public class IntermediateSupervision
    {

        public int SupervisionID { get; set; }
        public string AcademicStaffID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Year { get; set; }
        public double Hours { get; set; }
        public string Type { get; set; }
        public string Comments { get; set; }

        public IntermediateSupervision()
        {

        }
    }
}