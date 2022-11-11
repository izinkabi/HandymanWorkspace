using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.DataAccess.Query;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        IBusinessData _businessData;
        public DeliveryController(IBusinessData business)
        {
            _businessData = business;
        }

        [HttpGet]
        public EmployeeModel Get(string employeeid)
        {
            try
            {
                var employee = _businessData.GetEmployeeWithRatings(employeeid);
                return employee;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
