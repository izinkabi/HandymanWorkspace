using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HandymanUIApp.Models
{
    public class ServiceCategoryModel
    {
        //A category Model with a list of services
        public ServiceCategoryModel()
        {
            Services = new List<ServiceModel>();
        }
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

        public List<ServiceModel> Services { get;set; }
        
    }
}
