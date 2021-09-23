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
    
    public class IntermediateUnit
    {

        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string Area { get; set; }
        public bool PU_BaseHrsExtraFlag { get; set; }
        public bool PU_OtherTeachingFlag { get; set; }
        public bool ClientFlag { get; set; }
        public bool ExamFlag { get; set; }
        public bool LabFlag { get; set; }
        public bool FieldworkFlag { get; set; }
        public int Tier { get; set; }
        public double UCMTierBaseHrs { get; set; }
        public bool ActiveFlag { get; set; }
        public bool ProjectFlag { get; set; }
        public int CreditPoints { get; set; }
        public double CreditPointsRatio { get; set; }
        public string TeachingPattern { get; set; }

        public IntermediateUnit()
        {

        }
    }
}