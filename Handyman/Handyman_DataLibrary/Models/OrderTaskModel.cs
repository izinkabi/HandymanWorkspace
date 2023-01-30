namespace Handyman_DataLibrary.Models
{
    public class OrderTaskModel
    {
        public int ord_id { get; set; }
        public int ord_service_id { get; set; }
        public DateTime ord_datecreated { get; set; }
        public int ord_status { get; set; }
        public DateTime ord_duedate { get; set; }
        public int task_id { get; set; }
        public string? tas_title { get; set; }
        public DateTime tas_date_started { get; set; }
        public DateTime tas_date_finished { get; set; }
        public int tas_duration { get; set; }
        public int order_id { get; set; }
        public string? tas_description { get; set; }
        public int tas_status { get; set; }
    }
}
