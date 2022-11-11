using HandymanProviderLibrary.Models.Delivery;

namespace Handyman_SP_UI.Helpers
{
    public interface IEmployeeHelper
    {
        Task<EmployeeModel> LoadEmployee();
    }
}