using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public interface IBusinessData
    {
        BusinessModel CreateBusiness(BusinessModel business);
        void EmployServiceProvider(ServiceProviderModel serviceProvider);
        BusinessModel GetBusinessById(int? busId);
        void AddProviderServices(ServiceProviderModel provider);
        void ReleaseEmployee();
        ServiceProviderModel GetProvider(string employeeId);
    }
}