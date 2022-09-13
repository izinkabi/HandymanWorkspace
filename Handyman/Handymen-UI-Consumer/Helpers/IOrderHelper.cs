using Handymen_UI_Consumer.Models;

namespace Handymen_UI_Consumer.Helpers
{
    public interface IOrderHelper
    {
        Task<List<Service>> LoadServices();
        Task<List<Order>> LoadUserOrders();
        Task<Order> GetOrderById(int id);
        Task DeleteOrder(int Id);
        Task<List<TodoModel>> GetOrderTodoList(int id);
    }
}