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
        public int UserId { get; set; }
        public bool Approved  { get; set; }=false;
        public string? role_in_club { get; set; }
        public DateTime? ApprovedTime { get; set; }//onaylanma tarihi
        public DateTime CreateTime  { get; set; }=DateTime.Now;//istek atma tarihi


    }
}
