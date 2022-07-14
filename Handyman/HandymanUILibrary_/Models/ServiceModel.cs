using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary_.Models
{
    public class ServiceModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ServiceCategoryId { get; set; }

    }
}
