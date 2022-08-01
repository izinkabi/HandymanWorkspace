using System.ComponentModel.DataAnnotations;

namespace Handymen_UI_Consumer.Models
{
    public class Service
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Service Name")]
        public string? Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Service Description")]
        public string? Decription { get; set; }

        public int CategoryId { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Category Description")]
        public string? CategoryDescription { get; set; }
        public string? CategoryType { get; set; }

        [Display(Name = "Service Image")]
        public string? ImageUrl { get; set; }
        public string? Description { get; internal set; }
    }
}
