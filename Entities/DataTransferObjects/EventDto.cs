namespace Entities.DataTransferObjects
{
    public record EventDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }
    }
}
