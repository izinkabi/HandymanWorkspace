using System.Collections.Generic;

namespace HandymanUILibrary.Models.AuthModels;
public class RegisterModel
{

    public string Email { get; set; }


    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public List<string> Roles { get; set; } = new List<string> { "Consumer" };
}
