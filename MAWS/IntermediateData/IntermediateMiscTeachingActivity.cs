using MAWS.Models;
using System;

namespace MAWS.IntermediateData
{

    public class IntermediateMiscTeachingActivity
    {
        public int MiscTeachingActivityID { get; set; }
        public string AcademicStaffID { get; set; } // even if we dont show the id, would be useful
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Year { get; set; }
        public string UnitCode { get; set; }
        public string TeachingPeriod { get; set; }
        public string MiscName { get; set; }
        public double Hours { get; set; }
        public string Comments { get; set; }

        public IntermediateMiscTeachingActivity()
        {

        }
    }
}