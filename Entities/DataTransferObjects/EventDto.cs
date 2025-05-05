using Entities.Models;

namespace Entities.DataTransferObjects
{
    public record EventDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String? ImagePath { get; set; }
        public DateTime EventDate { get; set; }
        public String Location { get; set; }
        public string Visibility { get; set; }
        public string PublishedById { get; set; }
        public bool IsApproved { get; set; } = false;
        public string? ApprovedById { get; set; }
        public DateTime ApprovedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public Club Club { get; set; }// navigator prop
        public int ClubId { get; set; }//fk
        


    }
}
