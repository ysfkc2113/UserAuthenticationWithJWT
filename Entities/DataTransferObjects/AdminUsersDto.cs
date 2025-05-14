using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record AdminUsersDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilPhotoPath { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
        //public List<Club_User> Club_Users { get; set; } = new List<Club_User>(); // Bire çok ilişki
        public List<string>? RolesName { get; set; }= new List<string>();
    }
}
