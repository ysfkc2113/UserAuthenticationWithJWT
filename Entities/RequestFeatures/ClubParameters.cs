using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class ClubParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }

        public ClubParameters()
        {
            OrderBy = "ClubName";
        }
    }
}
