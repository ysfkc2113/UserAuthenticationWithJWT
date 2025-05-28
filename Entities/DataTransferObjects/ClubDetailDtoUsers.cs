using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ClubDetailDtoUsers
    {
        public ClubDto Club { get; set; }
        public List<EventDto> Events { get; set; }
        public int EventCount { get; set; }//clube ait etkinlik sayısı
        public int MemberCount { get; set; }//club üye sayısı
    }
}
