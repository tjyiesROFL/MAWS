using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class Unit
    {
        public Unit()
        {

        }

        [Key]
        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string UnitCode { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string UnitName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(6)")]
        public string Area { get; set; }

        [Required]
        public bool PU_BaseHrsExtraFlag { get; set; }

        [Required]
        public bool PU_OtherTeachingFlag { get; set; }

        [Required]
        public bool ClientFlag { get; set; }

        [Required]
        public bool ExamFlag { get; set; }

        [Required]
        public bool LabFlag { get; set; }

        [Required]
        public bool FieldworkFlag { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(1,0)")]
        public int Tier { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(5,2)")]
        public double UCMTierBaseHrs { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        [Required]
        public bool ProjectFlag { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(2)")]
        public int CreditPoints { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]
        public double CreditPointsRatio { get; set; }

        [Column(TypeName = "VARCHAR(6)")]
        public string TeachingPattern { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        public Discipline Discipline { get; set; }

        public List<UnitOffering> UnitOfferingList { get; set; }

        //teaching patterns?

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
