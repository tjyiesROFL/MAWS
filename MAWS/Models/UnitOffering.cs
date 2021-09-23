using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    //do need to update to fit new db table but don't want to disjoint UploadCSV func
    public class UnitOffering  //need to parse "Display Name" column from CSV to get actual year, mode, period, location.
    {
        public UnitOffering()
        {

        }

        [Key]
        [Required]
        [Column(TypeName = "VARCHAR(32)")]
        public string UnitOfferingID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string UnitCode { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(6)")]
        public string TeachingPeriod { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string Location { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(2)")]
        public string Mode { get; set; }

        [Required]
        public bool OUAE_Flag { get; set; }

        [Required]
        public bool WOCT_Flag { get; set; }

        [Required]
        public bool SGT_Flag { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(1)")]
        public string OfferingType { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        [Column(TypeName = "VARCHAR(8)")]
        public string AcademicStaffID {get; set;}
        public AcademicStaff AcademicStaff { get; set; }

        public Unit Unit { get; set; }
        
        //[Column(TypeName = "VARCHAR(6)")]
        //public TeachingPeroid TeachingPeroid{get; set;}

        public List<TeachingPattern> TeachingPatternList { get; set; }

        public List<TeachingActivity> TeachingActivityList { get; set; }

        public List<TeachingActivityAssignment> TeachingActivityAssignmentList { get; set; }

        public List<MiscTeachingActivity> MiscTeachingActivityList { get; set; }

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
