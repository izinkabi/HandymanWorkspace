namespace Handyman_SP_UI.Models
{
    public class OrderDisplayModel
    {

        public int Id { get; set; }
        public int requestId { get; set; }
        internal string? memberId { get; set; }
        public int ServiceId { get; set; }
        public string? Status { get; set; } = string.Empty;
        public int IsDelivered { get; set; }
        public DateTime DateAccepted { get; set; }
    }
}
