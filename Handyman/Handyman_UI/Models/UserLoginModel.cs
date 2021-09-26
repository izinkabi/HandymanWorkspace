﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class UserLoginModel
    {
       [Display(Name = "Username")]
       [Required(ErrorMessage ="Please enter your username/email")]
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        [Required(ErrorMessage = "Please enter your username/email")]
        public string Password { get; set; }
    }
}