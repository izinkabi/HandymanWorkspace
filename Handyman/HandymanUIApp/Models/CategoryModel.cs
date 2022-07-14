using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HandymanUIApp.Models
{
    public class CategoryModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string? Type { get; set; }

    }
}
