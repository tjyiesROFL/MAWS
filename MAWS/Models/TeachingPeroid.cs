using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MAWS.Models
{
    public class TeachingPeroid
    {
        public TeachingPeroid(){}

        [Key]
        [Column(TypeName = "VARCHAR(2)")]
        public string Peroid {get; set;}

        [Column(TypeName = "VARCHAR(30)")]
        public string PeroidName{get; set;}

        [Column(TypeName = "VARCHAR(30)")]
        public string Loaction {get; set;}

        [Column(TypeName="Numeric(2,0")]
        public int NoTeachingWeeks {get; set;}

        public bool TNE_Flag {get;set;}

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

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