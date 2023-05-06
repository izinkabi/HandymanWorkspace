using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HandymanProviderLibrary.Models.AuthModels;
public class LoginModel
{
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
