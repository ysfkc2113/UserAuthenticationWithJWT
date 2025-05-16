using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record AcademicianClubUserDtoInsertion
    {
        [Required]
        public string UserName { get; set; }
        public string? role_in_club { get; set; }
     }

}
