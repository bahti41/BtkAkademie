namespace Entities.DTOs
{
    public record BookDTO
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }
    }
}
