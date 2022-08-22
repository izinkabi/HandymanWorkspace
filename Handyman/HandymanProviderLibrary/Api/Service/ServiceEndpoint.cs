using HandymanProviderLibrary.API;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.Service;

public class ServiceEndpoint : IServiceEndpoint
{
    private IAPIHelper _aPIHelper;

    /// <summary>
    /// Constractor for the API helper
    /// </summary>
    /// <param name="aPIHelper"></param>
    /// 

    public ServiceEndpoint(IAPIHelper aPIHelper)
    {
        _aPIHelper = aPIHelper;
    }
    /// <summary>
    /// This method is used to Get a list of Service from the API
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ServiceModel>> GetServices()
    {
        try
        {
            List<ServiceModel> httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ServiceModel>>("Services/GetServies");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }


    //Get the provider's service(s) by provider Id
    public async Task<List<ProviderServiceModel>> GetProviderServicesByServiceId(int serviceId)
    {
        try
        {
            List<ProviderServiceModel>? httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ProviderServiceModel>>($"api/GetProvidersServicesByServiceId?serviceId={serviceId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Identity shall be used in the api to get the userId and hence up security breach
    //GET a list of provider's service (a list of providers that are providing a certaing service)
    public async Task<List<ProviderServiceModel>> GetProviderServiceByProviderId()
    {
        try
        {
            List<ProviderServiceModel>? httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ProviderServiceModel>>("api/GetProvidersServicesByProviderId");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Create a new service provided by the give provider
    public async Task CreateProviderService(ProviderServiceModel providerService)
    {
        try
        {
            var httpResponseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync<ProviderServiceModel>("api/GetProvidersServicesByProviderId", providerService);         
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Upadate the provider's service
    public async Task UpdateProviderService(ProviderServiceModel providerService)
    {
        try
        {
            var httpResponseMessage = await _aPIHelper.ApiClient.PutAsJsonAsync<ProviderServiceModel>("api/UpdateProvidersService", providerService);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
