using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record ClubManagerDtoForUpdate
    {
        [Required(ErrorMessage = "ClubId is required")]
        public int ClubId { get; set; }
        public string? ClubName { get; set; }
        public string? Description { get; set; }
        public string? Logo_url { get; set; }
    }
}
