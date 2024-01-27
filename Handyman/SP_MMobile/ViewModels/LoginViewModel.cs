using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SP_MLibrary.Services.Implementation;
using SP_MMobile.Views;
using System.ComponentModel.DataAnnotations;

namespace SP_MMobile.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    readonly AuthEndpoint authEndPoint;
    public LoginViewModel(AuthEndpoint authEndPoint)
    {
        authEndPoint = this.authEndPoint;
    }

    private SignInModel? LoginCredentialsInput = new SignInModel()!;
    [ObservableProperty]
    private string errorMsg = string.Empty;
    private string returnUrl;
    private bool IsSubmitted, IsRegistered, IsLoggedIn;


    [RelayCommand]
    public async Task Register()
    {
        ErrorMsg = string.Empty;
        try
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
        catch (Exception ex)
        {
            ErrorMsg = ex.Message;
        }

    }

    private class SignInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


    /// <summary>
    /// Sign in method for logging the user in
    /// by passing the input values to the back end Service
    /// </summary>
    [RelayCommand]
    private async Task SignIn()
    {
        ErrorMsg = string.Empty;
        if (LoginCredentialsInput != null &&
            LoginCredentialsInput.Username != null &&
                LoginCredentialsInput.Password != null)
        {
            try
            {

            }
            catch (Exception ex)
            {
                errorMsg = ("Invalid Credentials please try again");
                LoginCredentialsInput = new SignInModel();
                IsSubmitted = false;
                IsLoggedIn = false;
                errorMsg = ex.Message;
                //throw new Exception(ex.Message);
            }


        }
    }
}
