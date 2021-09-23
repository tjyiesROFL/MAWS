using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class MiscTeachingActivity_audit
    {
        public MiscTeachingActivity_audit()
        {
        }

        [Column(TypeName = "Timestamp",Order = 0)]
        public DateTime stamp  {get;set;}

        [Column(TypeName="Char(1)",Order = 1)]
        public char operation {get; set;}


        public int MiscTeachingActivityID { get; set; }

 
        [Column(TypeName = "NUMERIC(4,0)")]
        public int Year { get; set; }

        [Column(TypeName = "VARCHAR(12)")]
        public string UnitCode { get; set; }

        [Column(TypeName = "VARCHAR(12)")]
        public string TeachingPeriod { get; set; }

       
        [Column(TypeName = "VARCHAR(55)")]
        public string MiscName { get; set; }

       
        [Column(TypeName = "NUMERIC(6,2)")]
        public double Hours { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Comments { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]
 
        [Column(TypeName = "VARCHAR(32)")]
        public string UnitOfferingID { get; set; }


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
