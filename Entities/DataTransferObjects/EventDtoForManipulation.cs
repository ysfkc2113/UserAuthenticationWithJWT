using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record EventDtoForManipulation
    {
        [Required(ErrorMessage ="Title is a required field.")]
        [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters")]
        public String Title { get; set; }

       
        [MinLength(10, ErrorMessage = "Title must consist of at least 2 characters")]
        [MaxLength(200, ErrorMessage = "Title must consist of at maximum 50 characters")]
        [Required(ErrorMessage = "Description is required")]
        public String Description { get; set; }

        //public String? ImagePath { get; set; }//

        [Required(ErrorMessage = "EventDate is required")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public String Location { get; set; }
        public string Visibility { get; set; }

    }
}
