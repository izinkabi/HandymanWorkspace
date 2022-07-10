namespace HandymanUIApp.Models
{
    public class JobModel
    {
        public int Id { get; set; }
        public string? JobName { get; set; }
        public string? JobDecription { get; set; }
        public string? JobCategory { get; set; }

        public string JobImageUrl { get; set; }
    }
}
