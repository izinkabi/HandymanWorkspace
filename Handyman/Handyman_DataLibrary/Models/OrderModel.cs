
namespace Handyman_DataLibrary.Models
{
    //An order model with tasks model
    public class OrderModel
    {
        public string? ConsumerID { get; set; }
        public DateTime datecreated { get; set; }
        public int status { get; set; }
        public DateTime duedate { get; set; }
        public Service_CategoryModel? service { get; set; }
        public IList<TaskModel>? Tasks { get; set; }
        public int Id { get; set; }




      
    }
}
