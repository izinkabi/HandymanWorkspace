using HandymanUILibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Consumer.Order.Interface
{
    public interface IOrderEndPoint
    {
        //Task DeleteOrder(int Id);
        Task<List<OrderModel>> GetOrders(string customerId);
        Task<Task> PostOrder(OrderModel order);
        Task UpdateOrder(OrderModel orderUpdate);
    }
}