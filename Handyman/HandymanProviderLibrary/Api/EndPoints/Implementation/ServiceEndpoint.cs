﻿using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;


namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

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
            providerService.ServiceId,
            providerService.ServiceProviderId,
            providerService.Id
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
            var httpResponseMessage = await _aPIHelper.ApiClient.PutAsJsonAsync("api/UpdateProvidersService", providerService);
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

    //Insert a new Service
    public async Task<bool> InsertService(List<ServiceModel> services)
    {

        try
        {
            var result = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/services/postservices", services);
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
            var result = await _aPIHelper.ApiClient.PutAsJsonAsync("/api/services/UpdateService", serviceUpdate);
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
            var result = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/services/postprice", price);
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
            var priceId = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/service/insertbaseprice", price);
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

            PriceModel? priceModel = await _aPIHelper.ApiClient.GetFromJsonAsync<PriceModel>($"/api/services/getprice?priceId={priceId}");
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
            var result = await _aPIHelper.ApiClient.PostAsJsonAsync<ServiceModel>("/api/services/InsertCustomService", service);
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
            List<CustomServiceModel> customServices = await _aPIHelper.ApiClient.GetFromJsonAsync<List<CustomServiceModel>>($"/api/services/GetWorkShopServices?wsregId={wsregId}");
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
            var httpResponse = await _aPIHelper.ApiClient.PutAsJsonAsync<CustomServiceModel>("/api/services/updatewsservice", workshopService);
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