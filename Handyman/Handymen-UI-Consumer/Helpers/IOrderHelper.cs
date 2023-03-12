

using HandymanUILibrary.Models;

namespace Handymen_UI_Consumer.Helpers
{
    public interface IOrderHelper
    {

        Task<List<OrderModel>> LoadUserOrders(string userId);
        Task<OrderModel> GetOrderById(int id, string userId);
        Task DeleteOrder(OrderModel order);
        Task CreateOrder(OrderModel newOrder);
        Task UpdateOrderStatus(OrderModel order);
        void CancelOrder(OrderModel order);
        Task<int> NumberOfCancelledRequests();
        List<OrderModel> CancelledOrdersProperty { get; }

        Task<List<OrderModel>> GetOrdersOfDate(DateTime date);

    }
}