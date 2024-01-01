using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SP_MLibrary.Models;
using SP_MLibrary.Services.Interface;
using System.Collections.ObjectModel;

namespace SP_MMobile.ViewModels;

public partial class NewOrdersViewModel : BaseViewModel
{
    readonly IOrderEndpoint _orderEP;
    [ObservableProperty]
    string error;
    public NewOrdersViewModel(IOrderEndpoint orderEP)
    {
        _orderEP = orderEP;
        GetOrders();
    }

    [ObservableProperty]
    ObservableCollection<OrderModel>? orders;

    [RelayCommand]
    public async void GetOrders()
    {
        if (IsBusy) return;

        Orders = new ObservableCollection<OrderModel>();
        try
        {
            IsBusy = true;
            if (Error != null)
            {
                Error = string.Empty;
            }
            var newOrders = await _orderEP.GetNewOrdersByService(203);
            if (newOrders != null && newOrders.Count > 0)
            {
                foreach (var order in newOrders)
                {
                    if (order != null && !Orders.Contains(order))
                    {
                        Orders.Add(order);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            await Shell.Current.DisplayAlert("Error!", $"Unable to get Orders: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }


    }
}
