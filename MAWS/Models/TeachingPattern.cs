using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace MAWS.Models
{
    public class TeachingPattern
    {
        public TeachingPattern()
        {

        }

        [Key]
        [Required]
        [Column(TypeName = "VARCHAR(32)")]
        public string TeachingPatternID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string UnitCode { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4)")]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string TeachingPeriod { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(1)")]
        public string OfferingType { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4)")]
        public int TotalEnrolments { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4)")]
        public int ExternalEnrolments { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string EnrolmentStatus { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double WOCT_HrsPerSessionFIRST { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int WOCT_SessionsPerWeekFIRST { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double WOCT_HrsPerSessionREPEAT { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int WOCT_SessionsPerWeekREPEAT{ get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int SGT_ClassSize { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double SGT_HrsPerSession { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int SGT_SessionsPerWeek { get; set; }

        [Column(TypeName = "NUMERIC(1)")]
        public int UC_TNE_Affiliates { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int PU_GroupSize { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int PU_StaffAsClientQty { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int PU_StaffAsClientTNE_Qty { get; set; }
        public bool UC_New { get; set; }

        [Column(TypeName = "VARCHAR(5)")]
        public string UU_Type { get; set; }

        [Column(TypeName = "VARCHAR(1)")]
        public string UD_Type { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UD_DiscretionHrs { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string UD_DiscretionHrsComment { get; set; }

        [Column(TypeName = "NUMERIC(2)")]
        public int NoTeachingWeeks { get; set; }


        [Column(TypeName = "VARCHAR(255)")]
        public string UnitOfferingID { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        public UnitOffering UnitOffering { get; set; }

        [Column(TypeName = "VARCHAR(8)")]
        public string AcademicStaffID {get; set;}
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
