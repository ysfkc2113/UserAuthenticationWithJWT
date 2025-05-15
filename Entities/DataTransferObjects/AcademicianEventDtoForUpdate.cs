using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record AcademicianEventDtoForUpdate
    { 
            [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
            [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters")]
            public String? Title { get; set; }


            [MinLength(10, ErrorMessage = "Title must consist of at least 2 characters")]
            [MaxLength(200, ErrorMessage = "Title must consist of at maximum 50 characters")]
            public String? Description { get; set; }

            //public String? ImagePath { get; set; }//
            public DateTime? EventDate { get; set; }

            public String? Location { get; set; }
            public string? Visibility { get; set; }

    }
}






