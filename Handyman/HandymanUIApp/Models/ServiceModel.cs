using System.ComponentModel.DataAnnotations;

namespace HandymanUIApp.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Service Name")]
        public string? Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Service Description")]
        public string? Decription { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Category Type")]
        public string? CategoryType { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Category Description")]
        public string? CategoryDescription { get; set; }

        [Display(Name = "Service Image")]
        public string? ImageUrl { get; set; }
    }
}
