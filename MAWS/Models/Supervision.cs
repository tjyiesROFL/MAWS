using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MAWS.Models
{
    public class Supervision
    {
        public Supervision()
        {

        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupervisionID { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(6,2)")]
        public double Hours { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(3)")]
        public string Type { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Comments { get; set; }

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