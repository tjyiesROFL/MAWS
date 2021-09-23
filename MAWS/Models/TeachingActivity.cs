using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MAWS.Models
{
    public class TeachingActivity
    {
        public TeachingActivity()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeachingActivityID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string UnitCode { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string TeachingPeriod { get; set; }

        [Required]
        public bool Transferable { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string Activity { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int ActivityWeeks { get; set; }

        [Column(TypeName = "NUMERIC(3)")]
        public int ActivityQty { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(6,2)")]
        public int ActivityHrs { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        public UnitOffering UnitOffering { get; set; }

        public List<TeachingActivityAssignment> TeachingActivityAssignmentList { get; set; }


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
