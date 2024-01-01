using SP_MMobile.ViewModels;

namespace SP_MMobile.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginVM)
    {
        InitializeComponent();
        BindingContext = loginVM;

    }
}