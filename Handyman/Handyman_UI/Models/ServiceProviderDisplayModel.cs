﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Models
{
    public class ServiceProviderDisplayModel
    {
        

        [DataType(DataType.Text)]
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please your company name")]
        public string CompanyName { get; set; }

        
        [Display(Name = "Service Provider Types")]
        //[Required(ErrorMessage = "Select a service")]
        public IEnumerable<SelectListItem> serviceProviderTypes { get; set; }

        
        public int? ServiceProviderTypesId { get; set; }

        public string ProviderType { get; set; }

        public IEnumerable<SelectListItem> ServicesSelectList { get; set; }
        public int? SelectedServiceId { get; set; }

        public List<ServiceDisplayModel> services { get; set; }
        public List<ServiceDisplayModel> providerServices { get; set; }
    }
}