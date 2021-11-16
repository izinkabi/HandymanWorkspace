using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class ServiceDisplayModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string displayId { get; set; }
        public string ServiceDescription { get; set; }
        public string ImageUrl { get; set; }
    }
}