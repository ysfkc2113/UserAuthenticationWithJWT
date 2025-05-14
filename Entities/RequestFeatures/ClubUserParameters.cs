using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class ClubUserParameters:RequestParameters
    {
        public bool? IsApproved { get; set; }
        public string? SearchTerm { get; set; }

        //çalışmıyor boş olduğunda düzgün çalışıyor
        public ClubUserParameters()
        {
            OrderBy = "CreatedTime";
        }
    }
}
