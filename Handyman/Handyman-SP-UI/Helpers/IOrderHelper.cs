using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Helpers
{
    public interface IOrderHelper
    {
        Task<bool> AcceptOrder(OrderModel newOrder);
        int CheckStatus(OrderModel Order);
        Task<OrderModel> ConfirmAccepted(OrderModel OrderChecked);
        Task<IList<OrderModel>> GetCancelledOrders(string empID);
        Task<List<OrderModel>> GetCurrentMonthOrders();
        Task<List<OrderModel>> GetCurrentWeekOrders();
        Task<OrderModel> GetNewOrder(int serviceId, int orderId);
        Task<IList<OrderModel>> GetNewOrders(int serviceId);
        Task<OrderModel> GetOwnOrder(int id);

        int GetProgress(OrderModel Order);
        Task<TaskModel> GetTask(int id);
        Task<PriceModel> GetTaskPrice(int TaskId);
        Task UpdateTask(TaskModel taskUpdate);
    }
}