using Microsoft.AspNetCore.Identity;

namespace HR_Management.Models
{
    public class RoleProfile
    {
        public int Id { get; set; }
        public int TaskId { get; set; } // Foriegn key from SystemProfile
        public SystemProfile Task { get; set; }
        public string RoleId { get; set; } // Foriegn key from IdentityRole
        public IdentityRole Role { get; set; }

    }
}
