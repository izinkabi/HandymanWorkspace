using System.ComponentModel.DataAnnotations;

namespace Handyman_Api.Controllers;

public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}