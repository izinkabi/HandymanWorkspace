using SP_MMobile.ViewModels;

namespace SP_MMobile.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel registerVM)
    {
        InitializeComponent();
        BindingContext = registerVM;
    }
}