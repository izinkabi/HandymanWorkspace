using System.ComponentModel.DataAnnotations;

namespace Handyman_DataLibrary.Models;
public class LoginModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
    public string? UserId { get; set; }
    public string? Role { get; set; }


}
