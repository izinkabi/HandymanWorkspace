using HandymanUILibrary.Models;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.ServiceProvider
{
    public interface IRequestEndPoint
    {
        Task<RequestModel> PostRequest(RequestModel request);
        Task<RequestModel> GetRequest(string providerId);
        Task UpdateRequest(RequestModel requestUpdate);
        Task DeleteRequest(int Id);
    }
}