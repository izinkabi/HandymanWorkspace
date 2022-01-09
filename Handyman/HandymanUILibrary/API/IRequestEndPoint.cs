using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public interface IRequestEndPoint
    {
        Task<RequestModel> PostRequest(RequestModel request);
        Task<RequestModel> GetRequest(int customerId);
        Task UpdateRequest(RequestModel requestUpdate);
        Task DeleteRequest(int Id);
    }
}