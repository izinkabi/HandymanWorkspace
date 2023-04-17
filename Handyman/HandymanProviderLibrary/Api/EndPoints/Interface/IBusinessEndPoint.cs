using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IBusinessEndPoint
    {
        Task<BusinessModel> CreateNewBusiness(BusinessModel business);
        Task<bool> EmployMember(ServiceProviderModel serviceProvider);
        Task<BusinessModel> GetBusiness(int busId);
        Task<BusinessModel> GetWorkShop(string regNumber);
        Task<bool> InsertWorkShopService(int workShopRegId, int customServiceId);
    }
}