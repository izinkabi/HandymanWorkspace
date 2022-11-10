                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.Models
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
