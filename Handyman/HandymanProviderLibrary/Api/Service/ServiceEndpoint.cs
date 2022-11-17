using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.Service;

public class ServiceEndpoint : IServiceEndpoint
{
    static IAPIHelper _aPIHelper;
    static IList<ServiceModel>? services;
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
            if (services == null)
            {
                services = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ServiceModel>>("/api/services/GetServices");
            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return services.ToList();
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
    public async Task<List<ProviderServiceModel>> GetProviderServiceByProviderId(string providerId)
    {
        try
        {
            List<ProviderServiceModel>? httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ProviderServiceModel>>($"/api/GetProvidersServicesByProviderId?providerId={providerId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Create a new service provided by the give provider
    public async Task<string> CreateProviderService(ProviderServiceModel providerService)
    {
        string? result = null;
        var ps = new
        {
            ServiceId = providerService.ServiceId,
            ServiceProviderId = providerService.ServiceProviderId,
            Id = providerService.Id
        };

        try
        {
            var httpResponseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/PostProvidersService", ps);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                result = httpResponseMessage.ReasonPhrase;
                return result;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            return ex.Message;
        }
        return null;
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
    //Deleting the provider's service
    public async Task DeleteProviderService(ProviderServiceModel providerService)
    {
        try
        {
            var httpResponseMessage = await _aPIHelper.ApiClient.DeleteAsync($"api/UpdateProvidersService?providerService={providerService}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
