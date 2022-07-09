namespace HandymanUIApp.Models
{
    public class Order
    {
        internal int Id { get; set; }
        public string? JobName { get; set; }
        public string? JobDescription { get; set; }
        public DateTimeOffset Date { get; set; }
        internal bool IsDelivered { get; set; }
    }
}
