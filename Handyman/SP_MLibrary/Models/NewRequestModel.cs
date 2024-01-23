namespace SP_MLibrary.Models;


public class NewRequestModel
{
    public int ord_id { get; set; }
    public DateTime ord_datecreated { get; set; }
    public int ord_service_id { get; set; }
    public DateTime ord_duedate { get; set; }
    public int ord_status { get; set; }

}

