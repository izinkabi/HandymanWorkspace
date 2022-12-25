using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IRequestData
    {
        //IList<NewRequestModel> GetNewRequests(int serviceId);
        void InsertRequest(RequestModel request);
        IList<RequestModel> GetRequests(string providerId);
        //IList<TaskModel> GetTasks(int orderId);
    }

}