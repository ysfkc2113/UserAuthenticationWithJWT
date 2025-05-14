using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Club_User
    {
        public int Id { get; set; }
        public Club Club { get; set; }//fk club
        public int ClubId { get; set; }
        public User User { get; set; }//fk user 
        public string UserId { get; set; }
        public bool Approved  { get; set; }=false;
        public string? role_in_club { get; set; }
        public DateTime? ApprovedTime { get; set; } = DateTime.MinValue;//onaylanma tarihi
        public DateTime CreatedTime  { get; set; }//istek atma tarihi


    }
}
