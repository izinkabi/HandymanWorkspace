namespace Handyman_Api.Controllers;

public class PasswordResetModel
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}