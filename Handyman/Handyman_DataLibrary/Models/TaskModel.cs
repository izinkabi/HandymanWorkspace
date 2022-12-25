
namespace Handyman_DataLibrary.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public DateTime dateStarted { get; set; }
        public DateTime dateFinished { get; set; }
        public int duration { get; set; }
        public int serviceId { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int status { get; set; }
    }
}
