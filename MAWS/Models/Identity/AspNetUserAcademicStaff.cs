using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAWS.Models
{
    public class AspNetUserAcademicStaff
    {
        public AspNetUserAcademicStaff()
        {
        }

        [Key]
        [Required]
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Column(TypeName = "text")]
        public string StaffID { get; set; }
        public AcademicStaff AcademicStaff { get; set; }

        //-------------------------------------------------------------------- Audit Variables

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
