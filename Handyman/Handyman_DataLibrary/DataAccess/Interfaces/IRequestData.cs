using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IRequestData
    {
        IList<RequestModel> GetNewRequests(int serviceId);
        void InsertRequest(OrderModel request);
        IList<OrderModel> GetRequests(string providerId);
        TaskModel GetTask(int Id);
        //void UpdateRequest(RequestModel requestUpdate);
        void UpdateTask(TaskModel taskUpdate);
        OrderModel GetRequest(int id);
        IList<OrderModel> GetCurrentMonthRequests(string userId);
        IList<OrderModel> GetCurrentWeekRequests(string empID);
        IList<OrderModel> GetCancelledRequests(string empID);
    }

}