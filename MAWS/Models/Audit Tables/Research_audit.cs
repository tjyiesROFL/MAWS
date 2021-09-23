using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class Research_audit
    {
        public Research_audit() //class unfinished, need to check excel for colum titles
        {

        }


        [Column(TypeName = "text")]
        public string ResearchID { get; set; }

        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }


        [Column(TypeName = "NUMERIC(3,2)")]
        public double Fifteen_Pc { get; set; }


        [Column(TypeName = "NUMERIC(3,2)")]
        public double ECR_Pc { get; set; }


        [Column(TypeName = "NUMERIC(3,2)")]
        public double Income_Pc { get; set; }


        [Column(TypeName = "NUMERIC(3,2)")]
        public double Completions_Pc { get; set; }


        [Column(TypeName = "NUMERIC(3,2)")]
        public double Pubs_Pc { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double RCI_Pc { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double? Discretionary_Pc { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Discretionary_Comments { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double? Percentage {get; set;}

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
        
        [Column(TypeName = "Timestamp",Order = 0)]
        public DateTime stamp  {get;set;}

        [Column(TypeName="Char(1)",Order = 1)]
        public char operation {get; set;}
    }
}