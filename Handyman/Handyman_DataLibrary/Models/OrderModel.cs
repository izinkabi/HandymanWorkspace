
namespace Handyman_DataLibrary.Models
{
    //An order model with tasks model
    public class OrderModel
    {
        public string ConsumerID { get; set; }
        public DateTime ord_datecreated { get; set; }
        public int ord_status { get; set; }
        public DateTime ord_duedate { get; set; }
        public ServiceModel service { get; set; }
        public IList<TaskModel>? Tasks { get; set; }
        public int Id { get; set; }
    }
}
