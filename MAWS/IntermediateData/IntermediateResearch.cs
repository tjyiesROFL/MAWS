using MAWS.Models;
using System;

namespace MAWS.IntermediateData
{

    public class IntermediateResearch
    {
        public string ResearchID { get; set; }
        public string AcademicStaffID { get; set; } // even if we dont show the id, would be useful
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Year { get; set; }
        public double ECR_Pc { get; set; }
        public double Income_Pc { get; set; }
        public double Completions_Pc { get; set; }
        public double Fifteen_Pc { get; set; }
        public double Pubs_Pc { get; set; }
        public double RCI_Pc { get; set; }
        public double? Discretionary_Pc { get; set; }
        public string Discretionary_Comments { get; set; }
        public double? Percentage { get; set; }




                                             
        public IntermediateResearch()
        {

        }
    }
}