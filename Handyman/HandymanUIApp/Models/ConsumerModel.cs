namespace HandymanUIApp.Models
{
    public class ConsumerModel
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        internal AddressModel? ConsumerAddress { get; set; }
        public DateTimeOffset DateCreated { get; set; }

    }
}
