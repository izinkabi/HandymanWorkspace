using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IRequestData
    {
        IList<OrderModel> GetNewRequests(int serviceId);
        void InsertRequest(RequestModel request);
        IList<RequestModel> GetRequests(string providerId);
        TaskModel GetTask(int Id);
        //void UpdateRequest(RequestModel requestUpdate);
        void UpdateTask(TaskModel taskUpdate);
        RequestModel GetRequest(int id);
        IList<RequestModel> GetCurrentMonthRequests(string userId);
        IList<RequestModel> GetCurrentWeekRequests(string empID);
        IList<RequestModel> GetCancelledRequests(string empID);
    }

}