using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Handyman_UI.Models
{
    public class ActivationModel
    {
        public int Id { get; set; }

        [Display(Name = "Activation Code")]
        [Required(ErrorMessage = "Please enter the activation code sent to the phone number")]
        public int ActivationCode { get; set; }

        public DateTime ActivationDate { get; set; }
    }
}