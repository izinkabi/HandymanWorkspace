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

        //Get the business under which the employee(userId) which is a provider is registered
        [HttpGet]
        public ServiceProviderModel Get(string employeeid)
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
        public int Post(BusinessModel business)
        {
            try
            {
              int businessId = _businessData.CreateBusiness(business);
                return businessId;
            }catch(Exception ex) 
            {
                throw new Exception(ex.Message);    
            }
        }

        //Update the business or its address
        [HttpPut]
        public void Update()
        {

        }

        //Employ a new member
        [HttpPost]
        [Route("AddNewMember")]
        public void Employ(ServiceProviderModel serviceProvider)
        {
            try
            {
                _businessData.EmployMember(serviceProvider);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);    
            }
           
        }



    }
}
