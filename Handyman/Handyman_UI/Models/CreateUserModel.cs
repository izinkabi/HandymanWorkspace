using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class CreateUserModel
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter your username/email")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm your Password")]
        public string ConfirmPassword { get; set; }
    }
}
    
