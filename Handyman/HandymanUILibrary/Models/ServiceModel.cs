using System;
using System.Collections.Generic;

namespace HandymanUILibrary.Models
{
    public class ServiceModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<CustomServiceModel>? Customs { get; set; } = new()!;
        public string img { get; set; }
        public ServiceCategoryModel category { get; set; }
        public DateTime datecreated { get; set; }
        public string status { get; set; }
        public int PriceId { get; set; }


    }
}
