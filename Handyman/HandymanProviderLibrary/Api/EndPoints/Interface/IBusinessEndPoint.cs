using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Interface
{
    public interface IBusinessEndPoint
    {
        Task CreateNewBusiness(BusinessModel business);
        Task EmployMember(ServiceProviderModel serviceProvider);
        Task<BusinessModel> GetLoggedInEmployee(string employeeid);
        //Task<ServiceProviderModel> GetProvider(string employeeId);
    }
}