using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        IEmployeeData _employeeData;
        public DeliveryController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpGet]
        public EmployeeModel Get(string employeeid)
        {
            try
            {
                var employee = _employeeData.GetEmployeeWithServices(employeeid);
                return employee;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
