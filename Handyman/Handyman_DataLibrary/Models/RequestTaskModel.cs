namespace Handyman_DataLibrary.Models
{
    public class RequestTaskModel
    {
        public int req_id { get; set; }
        public int req_service_id { get; set; }
        public DateTime req_datecreated { get; set; }
        public int req_status { get; set; }
        public DateTime req_duedate { get; set; }
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
