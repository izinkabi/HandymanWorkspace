namespace Handyman_DataLibrary.Models
{
    public class CustomServiceModel
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int originalServiceId { get; set; }
        public string? imageUrl { get; set; }
    }
}
