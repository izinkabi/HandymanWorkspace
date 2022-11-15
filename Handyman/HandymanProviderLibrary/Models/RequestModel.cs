﻿namespace HandymanProviderLibrary.Models
{
    public class RequestModel
    {

        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? ProviderId { get; set; }
        public int ServiceId { get; set; }
        public string? Status { get; set; } = string.Empty;
        public int IsDelivered { get; set; }
        public DateTime DateAccepted { get; set; }
    }
}
