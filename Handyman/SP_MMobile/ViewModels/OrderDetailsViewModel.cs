using CommunityToolkit.Mvvm.ComponentModel;
using SP_MLibrary.Models;
namespace SP_MMobile.ViewModels
{
    [QueryProperty("Order", "Order")]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        public OrderDetailsViewModel()
        {

        }

        [ObservableProperty]
        OrderModel order;
    }
}
