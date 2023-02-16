using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IBusinessEndPoint
    {
        Task<BusinessModel> CreateNewBusiness(BusinessModel business);
        Task EmployMember(ServiceProviderModel serviceProvider);
        Task<BusinessModel> GetBusiness(int busId);

    }
}