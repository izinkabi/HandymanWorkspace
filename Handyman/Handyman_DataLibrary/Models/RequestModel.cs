namespace Handyman_DataLibrary.Models;

public class RequestModel
{
    public int req_id { get; set; }
    public ServiceModel? Service { get; set; } = new ServiceModel();
    public DateTime req_datecreated { get; set; }
    public int req_status { get; set; }
    public int req_progress { get; set; }
    public string? req_employeeid { get; set; }
    public int req_orderid { get; set; }
    public List<TaskModel>? tasks { get; set; }
}
