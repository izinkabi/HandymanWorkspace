using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers;

[Route("api/services")]
[ApiController]
public class ServicesController : ControllerBase
{
    IServiceData _serviceData;
    public ServicesController(IServiceData serviceData)
    {
        _serviceData = serviceData;
    }

    [HttpGet]
    [Route("GetServices")]
    //[Authorize]
    public List<ServiceModel> Get()
    {
        return _serviceData.GetAllServices();
    }

    //[HttpGet]
    //public List<ServiceCategoryModel> GetValues()
    //{
    //    ServiceData service = new ServiceData();

    //    return service.GetServiceCategories();
    //}

}
