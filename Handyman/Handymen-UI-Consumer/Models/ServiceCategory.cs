using System.ComponentModel.DataAnnotations;

namespace Handymen_UI_Consumer.Models
{
    public class ServiceCategory
    {
        //A category Model with a list of services

        public int CategoryId { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { get; set; }

        public List<Service> Services { get; set; }
    }
}
