

namespace SP_MLibrary.Model;

public class TaskModel
{
    //--naming violation rules
    public int Id { get; set; }
    public int task_id { get; set; }
    public DateTime tas_date_started { get; set; }
    public DateTime tas_date_finished { get; set; }
    public int tas_duration { get; set; }
    public int tas_service_id { get; set; }
    public string? tas_title { get; set; }
    public string? tas_description { get; set; }
    public int tas_status { get; set; }

}
