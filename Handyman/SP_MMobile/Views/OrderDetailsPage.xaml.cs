using SP_MMobile.ViewModels;

namespace SP_MMobile.Views;

public partial class OrderDetailsPage : ContentPage
{
    public OrderDetailsPage(OrderDetailsViewModel orderDetalsVM)
    {
        InitializeComponent();
        BindingContext = orderDetalsVM;

    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}