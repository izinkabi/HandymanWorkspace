using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class ProfileModel
    {
        //public int Id { get; }
        public string UserId { get; set; }

        //public string Type { get; }

        [DataType(DataType.Text)]
        [Display(Name = "Name(s)")]
        [Required(ErrorMessage = "Please enter your Name(s)")]
        public string Name { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Please enter your Surname")]
        public string Surname { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Please enter your your date of birth")]
        public string DateTimeOfBirth { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Home Address")]
        [Required(ErrorMessage = "Please enter your Home Address")]
        public string HomeAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your phone number with a contry code example(+27 ---)")]
        public string PhoneNumber { get; set; }


        
        public int ActivationId { get; }

        public string Type { get; set; }
    }
}