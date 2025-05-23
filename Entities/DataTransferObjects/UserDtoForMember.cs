using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record UserDtoForMember
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public MailAddress? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilPhotoPath { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<string> RoleNames { get; set; } = new();
    }
}
