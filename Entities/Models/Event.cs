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
        public decimal Price { get; set; }
        public int ClubId { get; set; }//fk
        public Club Club { get; set; }// navigator prop


    }
}
