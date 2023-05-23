using HandymanUILibrary.API.User;
using HandymanUILibrary.Models;
using HandymanUILibrary.Models.Auth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HandymanUILibrary.API.Services;

public class ServiceEndPoint : IServiceEndPoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly AuthenticatedUserModel _authenticatedUser;
    private IList<ServiceModel>? services;
    /// <summary>
    /// Constractor for the API helper
    /// </summary>
    /// <param name="aPIHelper"></param>
    public ServiceEndPoint(IAPIHelper aPIHelper, AuthenticatedUserModel authenticatedUser)
    {
        _apiHelper = aPIHelper;
        _authenticatedUser = authenticatedUser;
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
            List<ServiceModel> services = new List<ServiceModel>();
            if (services != null && _authenticatedUser.Access_Token != null)
            {
                _apiHelper.ApiClient.DefaultRequestHeaders.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Clear();
                _apiHelper.ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applications/json"));
                _apiHelper.ApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authenticatedUser.Access_Token}");
                services = await _apiHelper.ApiClient.GetFromJsonAsync<List<ServiceModel>>("/api/services/GetServices");
            }

            return services;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


    }
    /// <summary>
    /// This method is used to get a list of service categories from the API
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ServiceCategoryModel>> GetServiceCategories()
    {
        try
        {
            List<ServiceCategoryModel> httpResponseMessage = await _apiHelper.ApiClient.GetFromJsonAsync<List<ServiceCategoryModel>>("Services/GetServiceCategories");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


    }
    /// <summary>
    ///This method is used to Find a service using it's ID from the API
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ServiceModel> GetServiceById(int id)
    {


        using (HttpResponseMessage httpResponseMessage = await _apiHelper.ApiClient.GetAsync($"/api/GetServiceById?Id={id}"))
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {

                var result = await httpResponseMessage.Content.ReadFromJsonAsync<ServiceModel>();

                return result;
            }
            else
            {
                throw new Exception(httpResponseMessage.ReasonPhrase);
            }
        }

    }

    //Update service
    public async Task UpdateService(ServiceModel service)
    {
        try
        {
            await _apiHelper.ApiClient.PutAsJsonAsync("/api/Services/UpdateService", service);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task RemoveCustom(ServiceModel service)
    {
        try
        {
            await _apiHelper.ApiClient.DeleteAsync($"/api/Services/DeleteCustom=service?{service}");
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<PriceModel> GetPrice(int priceId)
    {
        try
        {
            PriceModel price = await _apiHelper.ApiClient.GetFromJsonAsync<PriceModel>($"/api/services/GetPrice?priceId={priceId}");
            return price;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
