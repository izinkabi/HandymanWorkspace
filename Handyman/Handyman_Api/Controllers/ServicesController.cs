using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Handyman_Api.Controllers;

[ApiController]
[Route("api/services")]
public class ServicesController : ControllerBase
{
    IServiceData _serviceData;
    public ServicesController(IServiceData serviceData)
    {
        _serviceData = serviceData;
    }

    //Get service list
    [HttpGet]
    [Route("Getservices")]
    public IEnumerable<ServiceModel> Get()
    {
        return _serviceData.GetAllServices();
    }


    //Post new list of services
    [HttpPost]
    [Route("PostServices")]
    public void Post(List<ServiceModel> newServices)
    {
        try
        {
            _serviceData.InsertServices(newServices);
        }
        catch (Exception)
        {

            throw;
        }
    }

    //Insert a new custom service
    [HttpPost]
    [Route("InsertCustomService")]
    public int Post(ServiceModel newService)
    {
        try
        {
            return _serviceData.InsertCustomService(newService);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Update the price of the service by adding the negotiated price
    [HttpPut]
    [Route("UpdateNegotiatedPrice")]
    public void Put(int priceId, float nPrice)
    {
        try
        {
            _serviceData.InsertNegotiatedPrice(priceId, nPrice);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Get a service by id
    [HttpGet]
    [Route("GetService")]
    public ServiceModel GetService(int id) => _serviceData.GetService(id);

    //Update the service
    [HttpPut]
    [Route("UpdateService")]
    public void UpdateService(ServiceModel service)
    {
        try
        {
            _serviceData.UpdateService(service);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Insert new base-Price and get its id
    [HttpPost]
    [Route("InsertBasePrice")]
    public int InsertBasePrice(float price)
    {
        try
        {
            int priceId = _serviceData.InsertBasePrice(price);
            return priceId;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Get the price of the given id
    [HttpGet]
    [Route("GetPrice")]
    public PriceModel GetPrice(int priceId)
    {
        try
        {
            var price = _serviceData.GetPrice(priceId);
            return price;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    //Get workshop services / only by owner(s)
    [HttpGet]
    [Route("GetWorkShopServices")]
    public List<CustomServiceModel> GetWorkShopServices(int wsregId) => _serviceData.GetWorkShopServices(wsregId);

    //Update workshop service
    [HttpPut]
    [Route("Updatewsservice")]
    public void UpdateWorkShopService(CustomServiceModel wsService)
    {
        if (wsService is null)
        {
            return;
        }

        try
        {
            _serviceData.UpdateWorkshopService(wsService);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}

