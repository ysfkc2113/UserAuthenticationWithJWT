using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public record EventDtoForInsertion : EventDtoForManipulation
    {
        [Required(ErrorMessage ="ClubId is required")]
        public int ClubId { get; init; }
    }
}
