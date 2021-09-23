using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class TeachingActivityAssignment_audit
    {
        public TeachingActivityAssignment_audit()
        {

        }

        [Column(TypeName = "Timestamp",Order = 0)]
        public DateTime stamp  {get;set;}

        [Column(TypeName="Char(1)",Order = 1)]
        public char operation {get; set;}

        public int AssignmentID { get; set; }

        [Column(TypeName = "NUMERIC(6,2)")]
        public int ActivityHrs { get; set; }


        [Column(TypeName = "NUMERIC(6,2)")]
        public int WorkloadHrs { get; set; }

        public bool ActiveFlag { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        public int TeachingActivityID { get; set; }

        public string StaffID { get; set; }
        //---------------------------------------------------------------------------------------- Audit Variables

        [Column(TypeName = "text")]
        public string Create_User { get; set; }

        [Column(TypeName = "Timestamp")]
        public DateTime Create_DateTime { get; set; }

        [Column(TypeName = "text")]
        public string Update_User { get; set; }

        [Column(TypeName = "Timestamp")]
        public DateTime Update_DateTime { get; set; }

    }
}