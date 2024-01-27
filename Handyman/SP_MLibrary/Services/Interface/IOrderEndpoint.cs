using SP_MLibrary.Models;

namespace SP_MLibrary.Services.Interface
{
    public interface IOrderEndpoint
    {
        Task<IList<OrderModel>> GetCancelledOrders(string empID);
        Task<List<OrderModel>> GetCurrentMonthOrders(string empID);
        Task<List<OrderModel>> GetCurrentWeekOrders(string empID);
        Task<IList<OrderModel>> GetNewOrdersByService(int serviceId);
        Task<List<OrderModel>> GetOrdersByProvider(string providerId);
        Task<TaskModel> GetTask(int id);
        Task<PriceModel> GetTaskPrice(int TaskId);
        Task<bool> PostOrder(OrderModel Order);
        Task<bool> PostTaskPrice(int taskId, int priceId);
        Task<bool> UpdateTask(TaskModel taskUpdate);
    }
}