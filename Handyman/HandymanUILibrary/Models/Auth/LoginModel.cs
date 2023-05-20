using System.ComponentModel.DataAnnotations;

namespace HandymanUILibrary.Models.AuthModels;
public class LoginModel
{
    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    public bool RememberMe { get; set; }
    public string? UserId
    {
        get; set;
    }
    public string Role { get; set; } = "Consumer";
}
