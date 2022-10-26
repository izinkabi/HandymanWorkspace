
namespace Handyman_DataLibrary.Models
{
    //An order model with tasks model
    public class OrderModel
    {
        public string ConsumerID { get; set; }
        public DateTime ord_datecreated { get; set; }
        public string? ord_status { get; set; }
        public DateTime ord_duedate { get; set; }
        public int ord_service_id { get; set; }
        public IEnumerable<Task>? Tasks { get; set; }
        public int Id { get; set; }
    }
}
