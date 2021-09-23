using Microsoft.AspNetCore.Identity;

namespace MAWS.Models
{
    public class UserRole : IdentityRole
    {
        public UserRole()
        {

        }

        public string Description { get; set; }

    }
}