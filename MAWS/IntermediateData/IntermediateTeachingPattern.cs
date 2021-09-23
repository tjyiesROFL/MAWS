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
    
    public class IntermediateTeachingPattern
    {



        public string TeachingPatternID { get; set; }
        public string UnitCode { get; set; }
        public int Year { get; set; }
        public string TeachingPeriod { get; set; }
        public string OfferingType { get; set; }
        public int TotalEnrolments { get; set; }
        public int ExternalEnrolments { get; set; }
        public string EnrolmentStatus { get; set; }
        public double WOCT_HrsPerSessionFIRST { get; set; }
        public int WOCT_SessionsPerWeekFIRST { get; set; }
        public double WOCT_HrsPerSessionREPEAT { get; set; }
        public int WOCT_SessionsPerWeekREPEAT { get; set; }
        public int SGT_ClassSize { get; set; }
        public double SGT_HrsPerSession { get; set; }
        public int SGT_SessionsPerWeek { get; set; }
        public int UC_TNE_Affiliates { get; set; }
        public int PU_GroupSize { get; set; }
        public int PU_StaffAsClientQty { get; set; }
        public int PU_StaffAsClientTNE_Qty { get; set; }
        public bool UC_New { get; set; }
        public string UU_Type { get; set; }
        public string UD_Type { get; set; }
        public double UD_DiscretionHrs { get; set; }
        public string UD_DiscretionHrsComment { get; set; }
        public int NoTeachingWeeks { get; set; }
        public bool ActiveFlag { get; set; }
        public string UnitOfferingID { get; set; }

    }
}