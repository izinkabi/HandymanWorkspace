using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public interface IBusinessData
    {
        void EmployMember(EmployeeModel newEmployee);
        BusinessModel GetBusiness(int businessId);
        void Register(RegistrationModel registration);
        void ReleaseEmployee();
    }
}