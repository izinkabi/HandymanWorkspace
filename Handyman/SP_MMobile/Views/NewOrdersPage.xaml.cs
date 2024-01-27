using SP_MMobile.ViewModels;

namespace SP_MMobile.Views;

public partial class NewOrdersPage : ContentPage
{
    public NewOrdersPage(NewOrdersViewModel newOrderVM)
    {
        InitializeComponent();
        BindingContext = newOrderVM;
    }


}