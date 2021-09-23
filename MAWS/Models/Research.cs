using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class Research
    {
        public Research() //class unfinished, need to check excel for colum titles
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ResearchID { get; set; }

        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]

        public double Fifteen_Pc { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]

        public double ECR_Pc { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]

        public double Income_Pc { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]

        public double Completions_Pc { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]

        public double Pubs_Pc { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]

        public double RCI_Pc { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double? Discretionary_Pc { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Discretionary_Comments { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double? Percentage {get; set;}

        [Required]
        public bool IS_CURRENT { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]
        
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