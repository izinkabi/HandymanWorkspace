namespace Handyman_DataLibrary.Models;

public class OrderModel
{
    public int order_id { get; set; }
    public ServiceModel? Service { get; set; } = new ServiceModel();
    public DateTime order_datecreated { get; set; }
    public int order_status { get; set; }
    public int order_progress { get; set; }
    public string? order_employeeid { get; set; }
    public int order_requestid { get; set; }
    public List<TaskModel>? tasks { get; set; }
}
