using HandymanUILibrary_.Models;

namespace HandymanUILibrary_.API.EndPoints
{
    public interface IServiceEndpoint
    {
        Task<List<ServiceModel>> GetAllServices();
    }
}