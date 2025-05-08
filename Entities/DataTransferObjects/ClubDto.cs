using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ClubDto
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string Description { get; set; }
        public string? Logo_url { get; set; }
        public string Faculty { get; set; }       
        public string? Responsible_teacher { get; set; }
        public string? ClubManager { get; set; }

    }
}
