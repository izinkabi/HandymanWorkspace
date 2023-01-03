

using HandymanUILibrary.Models;

namespace Handymen_UI_Consumer.Helpers
{
    public interface IOrderHelper
    {

        Task<List<OrderModel>> LoadUserOrders();
        Task<OrderModel> GetOrderById(int id);
        Task DeleteOrder(OrderModel order);
        Task CreateOrder(OrderModel newOrder);

    }
}