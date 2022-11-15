using System;

namespace HandymanUILibrary.Models
{
    public class ServiceModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public ServiceCategoryModel category { get; set; }
        public DateTime datecreated { get; set; }
        public string status { get; set; }

    }
}
