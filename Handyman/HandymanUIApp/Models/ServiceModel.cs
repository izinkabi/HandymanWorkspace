using System.ComponentModel.DataAnnotations;

namespace HandymanUIApp.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Service Name")]
        public string? Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Service Description")]
        public string? Decription { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Service Category")]

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public string? CategoryType { get; set; }

        [Display(Name = "Service Image")]
        public string? ImageUrl { get; set; }
        public string? Description { get; internal set; }
    }
}
