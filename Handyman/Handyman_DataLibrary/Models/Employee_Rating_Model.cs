using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman_DataLibrary.Models
{
    public class Employee_Rating_Model
    {
        public string? emp_Id { get; set; }

        
        public int emp_ratingid { get; set; }

        public int rate_id { get; set; }
        public int rate_stars { get; set; }
        public string rate_review { get; set; }
        public string rate_recommendation { get; set; }
        
    }
}
