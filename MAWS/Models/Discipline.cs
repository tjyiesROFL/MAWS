using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class Discipline
    {


        public Discipline()
        {

        }


        [Key]
        [Required]
        [Column(TypeName = "VARCHAR(6)")]
        public string DisciplineID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string DisciplineName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(6)")]
        public string School { get; set; }

        [Required]
        public bool ActiveFlag { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double WOCT_FirstHrsPH { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double WOCT_RepeatHRsPH { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double SGT_FirstSessionCount { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double SGT_FirstHrsPH { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double SGT_SubsequentHrsPH { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double SGT_MarkingHrsPS { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double Marking_ExamHrsPS { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double OUAE_AttentionHrsPS { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double OUAE_MarkingHrsPS { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double CP_3ptRatio { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double CP_6ptRatio { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double CP_9ptRatio { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double CP_12ptRatio { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double U_BaseHrs_Tier1 { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double U_BaseHrs_Tier2 { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double U_BaseHrs_Tier3 { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_ExternalHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_BaseStudents { get; set; }

        [Column(TypeName = "NUMERIC(6,5)")]
        public double UCM_AdditionalHrsPerStudent { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_NewUCHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_UpdateHrs_Minor { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_UpdateHrs_Major { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_DevelopNewUnitBaseHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_DevelopNewUnitDiscretionHrsMax { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_DevelopNewUnitDigitallyEnhancedHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double PU_BaseHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double PU_AdditionalClassHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double? PU_BaseHrsTNE { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double PU_BaseHrsExtra { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double PU_OtherTeaching { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double PU_SupervisorHrsPP { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double PU_StaffAsClientHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double PU_StaffAsClientHrsTNE { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_TNE_SetupHrs { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_TNE_CMBaseHrs { get; set; }

        [Column(TypeName = "NUMERIC(6,2)")]
        public double UCM_TNE_CMBaseStudents { get; set; }

        [Column(TypeName = "NUMERIC(3,2)")]
        public double UCM_TNE_CMBaseAffiliates { get; set; }

        [Column(TypeName = "NUMERIC(4,2)")]
        public double UCM_TNE_CMAdditionalHrsPerAffiliate { get; set; }

        //---------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        //-------------------------------------- Head Of Discipline

        [Column(TypeName = "VARCHAR(8)")]
        public string HeadOfDiscipline { get; set; }
        public AcademicStaff AcademicStaff { get; set; }

        //-------------------------------------- List of all Units under the Discipline

        public List<Unit> UnitList { get; set; }

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