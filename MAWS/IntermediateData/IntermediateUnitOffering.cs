using System;
using MAWS.Models;

namespace MAWS.IntermediateData
{

    
    public class IntermediateUnitOffering
    {

        public string UnitOfferingID { get; set; }
        public string UnitCode { get; set; }
        public int Year { get; set; }
        public string TeachingPeriod { get; set; }
        public string Location { get; set; }
        public string Mode { get; set; }
        public bool OUAE_Flag { get; set; }
        public bool WOCT_Flag { get; set; }
        public bool SGT_Flag { get; set; }
        public string OfferingType { get; set; }
        public bool ActiveFlag { get; set; }

        public IntermediateUnitOffering()
        {

        }


    }
}