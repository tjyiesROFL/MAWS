using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class TeachingActivityAssignment
    {
        public TeachingActivityAssignment()
        {

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string Activity { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(6,2)")]
        public int ActivityHrs { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(6,2)")]
        public int WorkloadHrs { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string UnitCode { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(6)")]
        public string TeachingPeriod { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        //Assigned Teaching Activity 
        public TeachingActivity TeachingActivity { get; set; }

        //Assigned Staff responsible for Teaching Tctivity
        public AcademicStaff AcademicStaff { get; set; }

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