using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Models
{
    public class ProvidersServiceDisplayModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ServiceProviderId { get; set; }
        public List<SelectListItem> services { get; set; }
        public int? SelectedServiceId { get; set; }


    }
}