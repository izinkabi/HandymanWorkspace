using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IServiceData
    {
        List<ServiceModel> GetAllServices();
        ServiceModel GetService(int id);
        ServiceModel GetServiceByOrder(int id);
        void InsertServices(List<ServiceModel> services);
        void UpdateService(ServiceModel serviceUpdate);
    }
}