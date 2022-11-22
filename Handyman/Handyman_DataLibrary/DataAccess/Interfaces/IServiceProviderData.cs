using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IServiceProviderData
    {
        void InsertServices(ServiceProviderModel provider);
<<<<<<< HEAD
        void RemoveService(int serviceId,string providerId);
        int InsertProvider(ServiceProviderModel serviceProvider);
=======
        void RemoveService(int serviceId, string providerId);
        void InsertProvider(ServiceProviderModel provider);
        ServiceProviderModel GetServiceProvider(string providerId);
>>>>>>> 1576c75f23d5518700009eba9f6c9919e7494c91
    }
}