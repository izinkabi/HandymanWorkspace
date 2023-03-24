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

    //Get service list
    [HttpGet]
    [Route("GetServices")]

    public List<ServiceModel> Get()
    {
        return _serviceData.GetAllServices();
    }


    //Post new list of services
    [HttpPost]
    [Route("PostServices")]
    public void Post(List<ServiceModel> newService)
    {
        try
        {
            _serviceData.InsertServices(newService);
        }
        catch (Exception)
        {

            throw;
        }
    }

    //update a service (in a case of new customs)
    [HttpPut]
    [Route("UpdateService")]
    public void Put(ServiceModel newService) => _serviceData.UpdateService(newService);



    //Get a service by id
    [HttpGet]
    [Route("GetService")]
    public ServiceModel GetService(int id) => _serviceData.GetService(id);




}
