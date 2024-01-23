using SP_MMobile.ViewModels;

namespace SP_MMobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(LoginViewModel loginVM)
        {
            InitializeComponent();
            BindingContext = loginVM;
        }
    }

}
