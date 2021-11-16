using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class ServiceModel: IServiceModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ServiceCategoryId { get; set; }

    }
}