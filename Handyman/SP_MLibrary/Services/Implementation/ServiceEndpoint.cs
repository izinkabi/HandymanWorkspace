using SP_MLibrary.Models;
using SP_MLibrary.Services.Interface;
using SP_MLibrary.Services.ServiceHelper;
using System.Net.Http.Json;


namespace SP_MLibrary.Services.Implementation;

public class ServiceEndpoint : IServiceEndpoint
{
    private readonly IServiceHelper _ServicesClient;
    private readonly AuthenticatedUserModel _authedModel;
    IList<ServiceModel> services;
    /// <summary>
    /// Constractor for the Services helper
    /// </summary>
    /// <param name="ServicesHelper"></param>
    /// 

    public ServiceEndpoint(IServiceHelper _serviceHelper, AuthenticatedUserModel authedModel)
    {
        _ServicesClient = _serviceHelper;
        _authedModel = authedModel;
    }
    /// <summary>
    /// This method is used to Get a list of Service from the Services
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ServiceModel>> GetServices()
    {
        try
        {
            //if (_authedModel.Access_Token != null)
            //{
            //    _ServicesClient.ServicesClient.DefaultRequestHeaders.Clear();
            //    _ServicesClient.ServicesClient.DefaultRequestHeaders.Accept.Clear();
            //    _ServicesClient.ServicesClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
            //    _ServicesClient.ServicesClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authedModel.Access_Token}");

            services = await _ServicesClient.ServicesClient.GetFromJsonAsync<List<ServiceModel>>("/Services/services/GetServices");
            //}


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return services.ToList();
    }


    //Get the provider's Service(s) by provider Id
    public async Task<List<MemberServiceModel>> GetProviderServicesByServiceId(int serviceId)
    {
        try
        {
            List<MemberServiceModel> httpResponseMessage = await _ServicesClient.ServicesClient.GetFromJsonAsync<List<MemberServiceModel>>($"Services/GetProvidersServicesByServiceId?serviceId={serviceId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Identity shall be used in the Services to get the userId and hence up security breach
    //GET a list of provider's Service (a list of providers that are providing a certaing Service)
    public async Task<List<MemberServiceModel>> GetProviderServiceByProviderId(string providerId)
    {
        try
        {
            List<MemberServiceModel> httpResponseMessage = await _ServicesClient.ServicesClient.GetFromJsonAsync<List<MemberServiceModel>>($"/Services/GetProvidersServicesByProviderId?providerId={providerId}");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Create a new Service provided by the give provider
    public async Task<string> CreateProviderService(MemberServiceModel providerService)
    {
        string result = null;
        var ps = new
        {
            providerService.ServiceId,
            providerService.MemberId,
            providerService.Id
        };

        try
        {
            var httpResponseMessage = await _ServicesClient.ServicesClient.PostAsJsonAsync("/Services/PostProvidersService", ps);
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

    //Upadate the provider's Service
    public async Task UpdateProviderService(MemberServiceModel providerService)
    {
        try
        {
            var httpResponseMessage = await _ServicesClient.ServicesClient.PutAsJsonAsync("Services/UpdateProvidersService", providerService);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //Deleting the provider's Service
    public async Task DeleteProviderService(MemberServiceModel providerService)
    {
        try
        {
            var httpResponseMessage = await _ServicesClient.ServicesClient.DeleteAsync($"Services/UpdateProvidersService?providerService={providerService}");
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
            var result = await _ServicesClient.ServicesClient.PostAsJsonAsync("/Services/services/postservices", services);
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

    //Update a Service 
    public async Task<bool> UpdateService(ServiceModel serviceUpdate)
    {
        try
        {
            var result = await _ServicesClient.ServicesClient.PutAsJsonAsync("/Services/services/UpdateService", serviceUpdate);
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
            var result = await _ServicesClient.ServicesClient.PostAsJsonAsync("/Services/services/postprice", price);
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
            var priceId = await _ServicesClient.ServicesClient.PostAsJsonAsync("/Services/service/insertbaseprice", price);
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

            PriceModel priceModel = await _ServicesClient.ServicesClient.GetFromJsonAsync<PriceModel>($"/Services/services/getprice?priceId={priceId}");
            PriceModel price = priceModel;
            return price;

        }
        catch (Exception ex)
        {
            return null;
            throw new Exception(ex.Message);
        }
    }

    //Create a  new custom Service of the given Service
    public async Task<int> CreateCustomService(ServiceModel service)
    {
        if (service is null)
        {
            return 0;
        }

        try
        {
            var result = await _ServicesClient.ServicesClient.PostAsJsonAsync("/Services/services/InsertCustomService", service);
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
            List<CustomServiceModel> customServices = await _ServicesClient.ServicesClient.GetFromJsonAsync<List<CustomServiceModel>>($"/Services/services/GetWorkShopServices?wsregId={wsregId}");
            return customServices;

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }
    //Update a custom Service 
    public async Task<bool> UpdateWorkShopService(CustomServiceModel workshopService)
    {
        try
        {
            if (workshopService is null)
            {
                return false;
            }
            var httpResponse = await _ServicesClient.ServicesClient.PutAsJsonAsync("/Services/services/updatewsservice", workshopService);
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
            var httpResponse = await _ServicesClient.ServicesClient.DeleteAsync($"/Services/services/DeleteWorkShopService?wsServiceId={wsServiceId}&wsRegId={wsRegId}");
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

    //Delete the original Service from provider
    public async Task<bool> DeleteMemberService(int wsServiceId, string providerId)
    {
        if (wsServiceId == 0 || providerId is null)
        {
            return false;
        }
        try
        {
            var httpResponse = await _ServicesClient.ServicesClient.DeleteAsync($"/Services/services/DeleteProviderService?wsServiceId={wsServiceId}&providerId={providerId}");
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
