using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class UsersParameters : RequestParameters
    {
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
        public string? SearchTerm { get; set; }

        //çalışmıyor boş olduğunda düzgün çalışıyor
        public UsersParameters()
        {
            OrderBy = "FirstName";
        }
    }

}
