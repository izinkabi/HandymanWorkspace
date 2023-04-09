using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IServiceData
    {
        List<ServiceModel> GetAllServices();
        ServiceModel GetService(int id);
        ServiceModel GetServiceByOrder(int id);
        void InsertServices(List<ServiceModel> services);
        int InsertCustomService(ServiceModel serviceUpdate);
        bool InsertNegotiatedPrice(int serviceId, float nPrice);
        void UpdateService(ServiceModel serviceUdpate);
        int InsertBasePrice(float price);
        PriceModel GetPrice(int priceId);
        List<CustomServiceModel> GetWorkShopServices(int workShopRegId);
        void UpdateWorkshopService(CustomServiceModel wsService);
    }
}