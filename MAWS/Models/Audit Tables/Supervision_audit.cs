using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MAWS.Models
{
    public class Supervision_audit
    {
        public Supervision_audit()
        {

        }
         [Column(TypeName = "Timestamp",Order = 0)]
        public DateTime stamp  {get;set;}

        [Column(TypeName="Char(1)",Order = 1)]
        public char operation {get; set;}

        public int SupervisionID { get; set; }

        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }

        [Column(TypeName = "NUMERIC(6,2)")]
        public double Hours { get; set; }

        [Column(TypeName = "VARCHAR(3)")]
        public string Type { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Comments { get; set; }

        public bool IS_CURRENT { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]
        [Column(TypeName = "VARCHAR(8)")]
        public string AcademicStaffID {get; set;}


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