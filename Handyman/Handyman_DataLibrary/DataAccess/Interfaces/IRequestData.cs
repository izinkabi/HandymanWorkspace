using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IRequestData
    {
        void DeleteRequestAndTasks(string consumerId, int RequestId);
        IList<RequestModel> GetRequest(int RequestId);
        IEnumerable<RequestModel> GetRequests(string consumerID);
        int InsertRequest(RequestModel Request);
        void UpdateRequest(RequestModel request);
    }
}