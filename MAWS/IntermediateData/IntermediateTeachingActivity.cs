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
    
    public class IntermediateTeachingActivity
    {

        public int TeachingActivityID { get; set; }
        public string UnitCode { get; set; }
        public int Year { get; set; }
        public string TeachingPeriod { get; set; }
        public bool Transferable { get; set; }
        public string Activity { get; set; }
        public int ActivityWeeks { get; set; }
        public int ActivityQty { get; set; }
        public int ActivityGroups { get; set; }
        public int ActivityHrs { get; set; }
        public bool ActiveFlag { get; set; }
        public string UnitOfferingID { get; set; }
        public IntermediateTeachingActivity()
        {

        }
    }
}