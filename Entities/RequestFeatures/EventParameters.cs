namespace Entities.RequestFeatures
{
    public class EventParameters : RequestParameters
    {
        public int? ClubId { get; set; }
        public bool? IsApproved {  get; set; }
        public DateTime? StartDate { get; set; }= DateTime.MinValue;
        public DateTime? EndDate { get; set; }=DateTime.MaxValue;
        public bool ValidDateRange => !StartDate.HasValue || !EndDate.HasValue || EndDate > StartDate;

        public string? SearchTerm { get; set; }

        public EventParameters()
        {
            OrderBy = "CreatedTime"; // Tarihe göre sıralama varsayılır
        }
    }
}
