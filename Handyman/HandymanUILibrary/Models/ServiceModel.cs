using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryId { get; set; }
        public string Type { get; set; }


    }
}
