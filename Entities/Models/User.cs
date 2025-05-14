using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User: IdentityUser
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime{ get; set; }
        public string? ProfilPhotoPath {  get; set; }
        public DateTime CreatedTime { get; set; }
        //public List<Club_User> Club_Users { get; set; } = new List<Club_User>(); // Bire çok ilişki
        //public ICollection<IdentityUserRole<string>> UserRoles { get; set; } = new List<IdentityUserRole<string>>();
        [NotMapped]
        public List<string> RoleNames { get; set; } = new();
        public bool IsActive { get; set; } = true;
    }
}
