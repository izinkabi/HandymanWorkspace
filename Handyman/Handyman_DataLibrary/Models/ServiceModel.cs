using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.Models
{
    public class ServiceModel
    {
        public int serv_id { get; set; }
        public string serv_name { get; set; }
        public string serv_img { get; set; }
        public int serv_categoryid { get; set; }
        public DateTime serv_datecreated { get; set; }
        public string serv_status { get; set; }
        public string cat_name { get; set; }
        public string cat_type { get; set; }
        public string cat_description { get; set; }
    }
}
