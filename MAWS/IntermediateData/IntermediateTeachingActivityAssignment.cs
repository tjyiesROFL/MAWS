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
    
    public class IntermediateTeachingActivityAssignment
    {

        public int AssignmentID { get; set; }
        public int ActivityHrs { get; set; }
        public int WorkloadHrs { get; set; }
        public bool ActiveFlag { get; set; }
        public int TeachingActivityID { get; set; }
        public string StaffID { get; set; }

        public string AcademicStaffID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public IntermediateTeachingActivityAssignment()
        {

        }
    }
}