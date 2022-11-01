using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers
{
    

    [ApiController]
    public class ServiceController : ControllerBase
    {
        IServiceData _serviceData;
        public ServiceController(IServiceData serviceData)
        {
            _serviceData = serviceData;
        } 

        [HttpGet]
        [Route("api/Service/GetServices")]
        //[Authorize]
        public  List<Service_CategoryModel> GetServices()
        {
            return  _serviceData.GetAllServices();
        }

        //[HttpGet]
        //public List<ServiceCategoryModel> GetValues()
        //{
        //    ServiceData service = new ServiceData();

        //    return service.GetServiceCategories();
        //}

    }
}
