using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class ServiceEndpoint : IServiceEndpoint
{
    private readonly IAPIHelper _apiClient;
    private readonly AuthenticatedUserModel _authedModel;
    IList<ServiceModel>? services;
    /// <summary>
    /// Constractor for the API helper
    /// </summary>
    /// <param name="aPIHelper"></param>
    /// 

    public ServiceEndpoint(IAPIHelper aPIHelper, AuthenticatedUserModel authedModel)
    {
        _apiClient = aPIHelper;
        _authedModel = authedModel;
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
            if (_authedModel.Access_Token != null)
            {
                _apiClient.ApiClient.DefaultRequestHeaders.Clear();
                _apiClient.ApiClient.DefaultRequestHeaders.Accept.Clear();
                _apiClient.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
                _apiClient.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authedModel.Access_Token}");

                services = await _apiClient.ApiClient.GetFromJsonAsync<List<ServiceModel>>("/api/services/GetServices");
            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return services.ToList();
    }


    //Get the provider's service(s) by provider Id
    public async Task<List<MemberServiceModel>> GetProviderServicesByServiceId(int serviceId)
    {
        try
        {
            List<MemberServiceModel>? httpResponseMessage = await _apiClient.ApiClient.GetFromJsonAsync<List<MemberServiceModel>>($"api/GetProvidersServicesByServiceId?serviceId={serviceId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Identity shall be used in the api to get the userId and hence up security breach
    //GET a list of provider's service (a list of providers that are providing a certaing service)
    public async Task<List<MemberServiceModel>> GetProviderServiceByProviderId(string providerId)
    {
        try
        {
            List<MemberServiceModel>? httpResponseMessage = await _apiClient.ApiClient.GetFromJsonAsync<List<MemberServiceModel>>($"/api/GetProvidersServicesByProviderId?providerId={providerId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Create a new service provided by the give provider
    public async Task<string> CreateProviderService(MemberServiceModel providerService)
    {
        string? result = null;
        var ps = new
        {
            providerService.ServiceId,
            providerService.MemberId,
            providerService.Id
        };

        try
        {
            var httpResponseMessage = await _apiClient.ApiClient.PostAsJsonAsync("/api/PostProvidersService", ps);
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
    public async Task UpdateProviderService(MemberServiceModel providerService)
    {
        try
        {
            var httpResponseMessage = await _apiClient.ApiClient.PutAsJsonAsync("api/UpdateProvidersService", providerService);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //Deleting the provider's service
    public async Task DeleteProviderService(MemberServiceModel providerService)
    {
        try
        {
            var httpResponseMessage = await _apiClient.ApiClient.DeleteAsync($"api/UpdateProvidersService?providerService={providerService}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Insert a new Service
    public async Task<bool> InsertService(List<ServiceModel> services)
    {

        try
        {
            var result = await _apiClient.ApiClient.PostAsJsonAsync("/api/services/postservices", services);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Update a service 
    public async Task<bool> UpdateService(ServiceModel serviceUpdate)
    {
        try
        {
            var result = await _apiClient.ApiClient.PutAsJsonAsync("/api/services/UpdateService", serviceUpdate);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Insert new price and return its id
    public async Task<int> InsertPrice(float price)
    {
        try
        {
            var result = await _apiClient.ApiClient.PostAsJsonAsync("/api/services/postprice", price);
            return await result.Content.ReadFromJsonAsync<int>();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Insert a new base-price and get its id
    //If the query is not successful the return 0
    public async Task<int> InsertBasePrice(float price)
    {
        try
        {
            var priceId = await _apiClient.ApiClient.PostAsJsonAsync("/api/service/insertbaseprice", price);
            if (priceId.IsSuccessStatusCode)
            {
                return await priceId.Content.ReadFromJsonAsync<int>();
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Get the price of the given id
    public async Task<PriceModel> GetPrice(int priceId)
    {
        try
        {
            if (priceId == 0)
            { return null; }

            PriceModel? priceModel = await _apiClient.ApiClient.GetFromJsonAsync<PriceModel>($"/api/services/getprice?priceId={priceId}");
            PriceModel? price = priceModel;
            return price;

        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message);
        }
    }

    //Create a  new custom service of the given service
    public async Task<int> CreateCustomService(ServiceModel service)
    {
        if (service is null)
        {
            return 0;
        }

        try
        {
            var result = await _apiClient.ApiClient.PostAsJsonAsync<ServiceModel>("/api/services/InsertCustomService", service);
            if (result.IsSuccessStatusCode)
            {
                int customServiceId = await result.Content.ReadFromJsonAsync<int>();
                return customServiceId;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {

            return 0;
            throw new Exception(ex.Message);
        }
    }

    //Get workshop services of the given workshop registratiokn number
    public async Task<List<CustomServiceModel>> GetWorkShopServices(int wsregId)
    {
        if (wsregId == 0)
        {
            return null;
        }
        try
        {
            List<CustomServiceModel> customServices = await _apiClient.ApiClient.GetFromJsonAsync<List<CustomServiceModel>>($"/api/services/GetWorkShopServices?wsregId={wsregId}");
            return customServices;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
    //Update a custom service 
    public async Task<bool> UpdateWorkShopService(CustomServiceModel workshopService)
    {
        try
        {
            if (workshopService is null)
            {
                return false;
            }
            var httpResponse = await _apiClient.ApiClient.PutAsJsonAsync<CustomServiceModel>("/api/services/updatewsservice", workshopService);
            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }

    //Delete workshopservice
    public async Task<bool> DeleteWorkShopService(int wsServiceId, int wsRegId)
    {
        if (wsServiceId == 0 || wsRegId == 0)
        {
            return false;
        }

        try
        {
            var httpResponse = await _apiClient.ApiClient.DeleteAsync($"/api/services/DeleteWorkShopService?wsServiceId={wsServiceId}&wsRegId={wsRegId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message);
        }
    }

    //Delete the original service from provider
    public async Task<bool> DeleteMemberService(int wsServiceId, string providerId)
    {
        if (wsServiceId == 0 || providerId is null)
        {
            return false;
        }
        try
        {
            var httpResponse = await _apiClient.ApiClient.DeleteAsync($"/api/services/DeleteProviderService?wsServiceId={wsServiceId}&providerId={providerId}");
            if (httpResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
