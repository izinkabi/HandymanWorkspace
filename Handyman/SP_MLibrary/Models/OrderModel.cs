namespace SP_MLibrary.Models
{
    public class OrderModel
    {
        public string? ConsumerID { get; set; }
        public DateTime datecreated { get; set; }
        public int status { get; set; }
        public DateTime duedate { get; set; }
        public ServiceModel? Service { get; set; }
        public IList<TaskModel>? Tasks { get; set; }
        public int Id { get; set; }

    }
}
