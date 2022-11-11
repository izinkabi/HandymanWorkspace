using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Interfaces
{
    public interface IEmployeeData
    {
        EmployeeModel GetEmployeeWithRatings(string EmployeeId);
        void InsertEmployee(EmployeeModel employee);
    }
}