using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record AdminEventDtoForUpdate
    {
        [Required]
        public int Id { get; set; }
        public bool? IsApproved { get; set; } = false;
        public String? Title { get; set; }
        public String? Description { get; set; }
        public String? ImagePath { get; set; }
        public DateTime? EventDate { get; set; }
        public String? Location { get; set; }
        public string? Visibility { get; set; }

    }
}
