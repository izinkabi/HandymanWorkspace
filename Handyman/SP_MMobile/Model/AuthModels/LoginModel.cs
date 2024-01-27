using System.ComponentModel.DataAnnotations;

namespace SP_MMobile.Model.AuthModels;
public class LoginModel
{
    [EmailAddress]
    public string? Email { get; set; }
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public bool? RememberMe { get; set; }
    [Key]
    public string? UserId
    {
        get; set;
    }
    public string Role { get; set; } = "ServiceProvider";
}
