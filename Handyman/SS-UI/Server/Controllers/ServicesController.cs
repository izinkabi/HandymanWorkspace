using HandymanUILibrary.API.Services;
using HandymanUILibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace SS_UI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ServicesController : Controller
{
    private readonly IServiceEndPoint _serviceEndPoint;

    public ServicesController(IServiceEndPoint serviceEndPoint)
    {
        _serviceEndPoint = serviceEndPoint;
    }

    public async Task<PriceModel> GetPrice(int priceId)
    {
        try
        {
            if (priceId is 0)
            {
                return null;
            }
            PriceModel price = await _serviceEndPoint.GetPrice(priceId);
            return price;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [HttpGet]
    public async Task<List<ServiceModel>> GetService()
    {
        try
        {
            List<ServiceModel> services = await _serviceEndPoint.GetServices();
            return services;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
