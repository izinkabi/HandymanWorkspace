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
        IBusinessData _businessData;
        public DeliveryController(IBusinessData business)
        {
            _businessData = business;
        }

        //Get the business under which the employee(userId) is registered
        [HttpGet]
        public EmployeeModel Get(string employeeid)
        {
            try
            {
                var business = _businessData.GetBusiness(employeeid);
                return business.Employee;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //Register A business
        [HttpPost]
        public void Post(BusinessModel business)
        {

        }

        //Update the business or its address
        [HttpPut]
        public void Update()
        {

        }
        //Employ a new member
        public void Employ() 
        {

        }



    }
}
