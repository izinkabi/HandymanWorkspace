using Handyman_DataLibrary.DataAccess.Interfaces;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public interface IServiceProviderData : IEmployeeData
    {
        void InsertServices(IEnumerable<IServiceProvider> services);
        void RemoveService(int serviceId);
    }
}