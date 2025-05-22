using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class ClubManagerEventParameters:RequestParameters
    {
        public DateTime? StartDate { get; set; } = DateTime.MinValue;
        public DateTime? EndDate { get; set; } = DateTime.MaxValue;
        public bool ValidDateRange => !StartDate.HasValue || !EndDate.HasValue || EndDate > StartDate;

        public string? SearchTerm { get; set; }

        public ClubManagerEventParameters()
        {
            OrderBy = "CreatedTime"; // Tarihe göre sıralama varsayılır
        }
    }
}
