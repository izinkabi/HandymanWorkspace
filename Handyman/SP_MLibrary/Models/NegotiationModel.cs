namespace SP_MLibrary.Models
{
    public class NegotiationModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DateCreated { get; set; }
        public int PriceId { get; set; }
        public int IsCompleted { get; set; }
        public DateTime Order_DueDate { get; set; }
        public DateTime LastDateModified { get; set; }

    }
}
