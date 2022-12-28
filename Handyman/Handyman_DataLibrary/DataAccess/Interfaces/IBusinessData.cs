using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public interface IBusinessData
    {
        int CreateBusiness(BusinessModel business);
        void EmployServiceProvider(ServiceProviderModel serviceProvider);
        BusinessModel GetBusiness(string userId);
        void AddProviderServices(ServiceProviderModel provider);
        void ReleaseEmployee();
        ServiceProviderModel GetProvider(string employeeId);
    }
}