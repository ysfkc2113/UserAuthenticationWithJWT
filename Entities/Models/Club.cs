using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Club
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string Description { get; set; }
        public string? Logo_url { get; set; }
        public string Faculty { get; set; }
        //public User Responsible_teacher { get; set; }//
        public string? Responsible_teacher { get; set; }//danışman
        //public User ClubManager { get; set; }
        public string? ClubManager { get; set; }//lider

        public DateTime CreatedTime { get; set; }

       // public List<Club_User> Club_Users { get; set; } = new List<Club_User>(); // Bire çok ilişki
        //public List<Event> Events { get; set; } = new List<Event>(); // Bir kulübün birden çok etkinliği olabilir.


    }
}
