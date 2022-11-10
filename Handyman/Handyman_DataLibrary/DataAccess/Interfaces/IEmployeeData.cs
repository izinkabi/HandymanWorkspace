using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IEmployeeData
    {
        EmployeeModel GetEmployeeWithServices(string EmployeeId);
    }
}