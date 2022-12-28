using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IServiceProviderData
    {
        //void InsertServices(ServiceProviderModel provider);
        void RemoveService(int serviceId, string providerId);
        void InsertProvider(ServiceProviderModel provider);
        ServiceProviderModel GetServiceProvider(string providerId);
    }
}