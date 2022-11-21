using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IServiceProviderData: IEmployeeData
    {
        void InsertServices(ServiceProviderModel provider);
        void RemoveService(int serviceId,string providerId);
        int InsertProvider(ServiceProviderModel serviceProvider);
    }
}