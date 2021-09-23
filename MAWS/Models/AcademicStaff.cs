using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace MAWS.Models
{
    public class AcademicStaff
    {
        public AcademicStaff()
        {
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StaffID { get; set; }

        [Column(TypeName = "VARCHAR(8)")]
        public string AcademicStaffID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string Surname { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(3)")]
        public string EmployeeType { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(3)")]
        public string Area { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(6)")]
        public string ClassCode { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")]
        public string ClassName { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(4,0)")]
        public int FTBaseHrs { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]
        public double WorkFraction { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public string EmployeeStatus { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        public string ContractExpiryDate { get; set; }

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]
        public double WorkMax_Pc { get; set; }

        [Column(TypeName = "Numeric(6,2)")]
        public double WorkHrs {get; set;}

        [Required]
        [Column(TypeName = "NUMERIC(3,2)")]
        public double TeachingMax_Pc { get; set; }

        //--------------------------------------------------------------------------------------------- [Object Relations] / [DB Table Relations]

        //-------------------------------------- ONLY EXISTS If Academic Staff is Head Of Discipline
        public Discipline Discipline { get; set; }

        //-------------------------------------- ONLY EXISTS If Academic Staff is Unit Coordinator to 1 or more UnitOfferings
        public List<UnitOffering> UnitOfferingList { get; set; }

        //-------------------------------------- ONLY EXISTS If Academic Staff is Unit Coordinator AND has created a teaching pattern 
        //------ We should stored this in a Unit Offering as it's tied directly to it.
        public List<TeachingPattern> TeachingPatternList { get; set; }

        //-------------------------------------- ONLY EXISTS If Academic Staff is Assigned to 1 or more Teaching Activities
        //public List<TeachingActivity> TeachingActivity { get; set; }

        //-------------------------------------- ONLY EXISTS If Academic Staff is Assigned 1 or more MiscTeachingActivity
        public List<MiscTeachingActivity> MiscTeachingActivityList { get; set; }

        //-------------------------------------- ONLY EXISTS If Academic Staff is Assigned 1 or more TeachingActivity
        public List<TeachingActivity> TeachingActivityList { get; set; }

        public List<TeachingActivityAssignment> TeachingActivityAssignmentList { get; set; }


        //-------------------------------------- ONLY EXISTS If Academic Staff has 1 or more Research
        public List<Research> ReasearchList { get; set; }
        //-------------------------------------- ONLY EXISTS If Academic Staff has 1 or more Service
        public List<Service> ServiceList { get; set; }
        //-------------------------------------- ONLY EXISTS If Academic Staff has 1 or more Supervision
        public List<Supervision> SupervisionList { get; set; }
        //-------------------------------------- Keep track of academic staff members with MAWS accounts
        public AspNetUserAcademicStaff AspNetUserAcademicStaff { get; set; }

        //--------------------------------------------------------------------------------------------------------- Audit Variables

        [Column(TypeName = "text")]
        public string Create_User { get ; set; }

        [Column(TypeName = "Timestamp")]
        public DateTime Create_DateTime { get; set; }

        [Column(TypeName = "text")]
        public string Update_User { get; set; }

        [Column(TypeName = "Timestamp")]
        public DateTime Update_DateTime { get; set; }

    }
}
