using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MAWS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }

        public string FullName { get; set; }
        public string RoleTitle { get; set; }
        [Column(TypeName = "VARCHAR(8)")]
        public string AcademicStaffID { get; set; }
        public AspNetUserAcademicStaff AspNetUserAcademicStaff { get; set; }

    }
}