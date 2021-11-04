using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HandymanDataLibrary.Models
{
    public class ServiceModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ServiceCategoryId { get; set; }

        public int Id { get; set; }

    }
}