using System.ComponentModel.DataAnnotations;

namespace Handyman_UI_Provider.Models.Services
{
    public class DisplayServiceModel
    {
            public int Id { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Service Name")]
            public string? Name { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Service Description")]
            public string? Description { get; set; }
            [DataType(DataType.Text)]
            [Display(Name = "Service Category")]
            //public CategoryModel? Category { get; set; }
            public string? CategoryName { get; set; }
            public string? CategoryDescription { get; set; }
            public string? CategoryType { get; set; }

            [Display(Name = "Service Image")]
        public string? ImageUrl { get; set; }
    }
}
