using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record AdminClubUserDtoInsertion
    {
        [Required]
        public int ClubId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string? role_in_club { get; set; }
    }

}
