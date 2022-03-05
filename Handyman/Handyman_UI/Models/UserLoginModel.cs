using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class UserLoginModel
    {
       [DataType(DataType.EmailAddress)]
       [Display(Name = "Username")]
       [Required(ErrorMessage ="Please enter your username/email")]
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}