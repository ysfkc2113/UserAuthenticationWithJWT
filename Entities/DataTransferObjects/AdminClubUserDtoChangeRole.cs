using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record AdminClubUserDtoChangeRole
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string role_in_club { get; set; }//dictionary yapılabilir.
    }
}
