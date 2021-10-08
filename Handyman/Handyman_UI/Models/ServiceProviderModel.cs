using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Handyman_UI.Models
{
    public class ServiceProviderModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your name(s)")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Please enter your Surname")]
        public string Surname { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Home Address")]
        [Required(ErrorMessage = "Please enter your Home Address")]
        public string HomeAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your Mobile Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Please enter your your date of birth")]
        public string DateOfBirth { get; set; }
        public string UserId { get; set; }
        

      
            public IEnumerable<SelectListItem> Services { get; set; }

            //for first dropdown selected value
            public string Selected1 { get; set; }

            //for second dropdown selected value
            public string SelectedService2 { get; set; }
        
    }
}