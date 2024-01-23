using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IOrderData
    {
        IList<OrderModel> GetCancelledOrders(string employeeId);
        IList<OrderModel> GetCurrentMonthOrders(string employeeId);
        IList<OrderModel> GetCurrentWeekOrders(string employeeId);
        IList<OrderModel> GetNewOrders(int serviceId);
        OrderModel GetOrder(int id);
        IList<OrderModel> GetOrders(string providerId);
        TaskModel GetTask(int Id);
        void InsertOrder(OrderModel Order);
        void UpdateOrder(OrderModel OrderUpdate);
        void UpdateTask(TaskModel taskUpdate);
    }
}