using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public interface IBusinessData
    {
        int CreateBusiness(BusinessModel business);
        void EmployMember(ServiceProviderModel serviceProvider);
        BusinessModel GetBusiness(string userId);
        //int Register(RegistrationModel registration);
        void ReleaseEmployee();
    }
}