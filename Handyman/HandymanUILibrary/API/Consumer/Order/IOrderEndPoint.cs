using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace HandymanUILibrary.API.Consumer.Order
{
    public interface IOrderEndPoint
    {
        Task<OrderModel> PostOrder(OrderModel order, List<TaskModel> todoList);
        Task DeleteOrder(int id);
        Task<List<OrderModel>> GetOrders(string customerId);
        Task UpdateOrder(OrderModel order);

    }
}