using HandymanProviderLibrary.Models;

namespace Handyman_UI_Provider.Hubs.ServiceDelivery
{
    public interface IServiceDelivery
    {
        Task<List<ProviderServiceModel>> FilterServiceProvidersByService(int id);
    }
}