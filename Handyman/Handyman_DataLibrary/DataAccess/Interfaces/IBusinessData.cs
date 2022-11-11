using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IBusinessData
    {
        void EmployMember(EmployeeModel newEmployee);
        BusinessModel GetBusiness(string userId);
        void Register(RegistrationModel registration);
        void ReleaseEmployee();
    }
}