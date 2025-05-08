using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract record ClubDtoForManipulation
    {

        [Required(ErrorMessage = "ClubName is a required field.")]
        [MinLength(2, ErrorMessage = "ClubName must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "ClubName must consist of at maximum 50 characters")]
        public String ClubName { get; set; }


        [MinLength(10, ErrorMessage = "Title must consist of at least 2 characters")]
        [MaxLength(200, ErrorMessage = "Title must consist of at maximum 50 characters")]
        [Required(ErrorMessage = "Description is required")]
        public String Description { get; set; }
        [Required]
        public String Faculty { get; set; }

        public String? Logo_url { get; set; }
        public String? ClubManager { get; set; }
        public string? Responsible_teacher { get; set; }
    }
}
