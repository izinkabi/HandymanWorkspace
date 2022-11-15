using HandymanProviderLibrary.Api.EndPoints.Implementation;
using HandymanProviderLibrary.Models;

namespace Handyman_SP_UI.Pages.Helpers
{
    public class EmployeeHelper
    {

        static EmployeeEndPoint? _employee;
        static EmployeeModel? employeeModel;

        public EmployeeHelper(EmployeeEndPoint? employee)
        {
            _employee = employee;
            var temp = _employee.Employee;
            if (temp != null)
            {
                employeeModel = temp;
            }
        }

        public EmployeeModel Employee { get => _ = Employee; }


    }
}
