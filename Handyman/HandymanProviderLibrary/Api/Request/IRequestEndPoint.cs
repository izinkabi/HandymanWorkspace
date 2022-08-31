using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.Request
{
    public interface IRequestEndPoint
    {
        Task<List<OrderModel>> GetAllOrdersByService(int serviceId);
    }
}