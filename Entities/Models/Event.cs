using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Event
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String? ImagePath { get; set; }
        public DateTime EventDate { get; set; }
       // public decimal Price { get; set; }//silebiliriz
        public String Location { get; set; }
        public string Visibility { get; set; } = "private";//0=private(kulüp içi),1=public(herkese açık)
        public string PublishedByUserName { get; set; }
        public bool IsApproved { get; set; } = false;
        public string? ApprovedByUserName { get; set; }
        public DateTime ApprovedTime { get; set; }= DateTime.MinValue;
        public DateTime CreatedTime { get; set; }
        public int ClubId { get; set; }//fk
        public Club Club { get; set; }// navigator prop


    }
}
