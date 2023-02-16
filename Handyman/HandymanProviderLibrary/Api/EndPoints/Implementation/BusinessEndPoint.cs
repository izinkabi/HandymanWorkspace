using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class BusinessEndPoint : IBusinessEndPoint
{
    IAPIHelper? _apiHelper;
    IServiceProviderEndPoint? _serviceProvider;
    BusinessModel? business;
    string? ErrorMsg;
    public BusinessEndPoint(IAPIHelper apiHelper, IServiceProviderEndPoint serviceProvider)
    {
        _apiHelper = apiHelper;
        _serviceProvider = serviceProvider;
    }

    //Get the business's employee(ServiceProvider)
    public async Task<BusinessModel>? GetBusiness(int busId)
    {
        try
        {
            if (busId > 0)
            {
                business = new();
                if (busId != null)
                {
                    business = await _apiHelper?.ApiClient?.GetFromJsonAsync<BusinessModel>($"/api/Delivery/GetBusiness?busId={busId}");
                }

            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return business;
    }

    //Create a new business
    public async Task<BusinessModel> CreateNewBusiness(BusinessModel business)
    {
        try
        {
            var response = await _apiHelper.ApiClient.PostAsJsonAsync($"/api/Delivery/Business/Create?", business);
            BusinessModel newBusiness;
            if (response.IsSuccessStatusCode)
            {
                newBusiness = response.Content.ReadFromJsonAsync<BusinessModel>().Result;
                return newBusiness;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    //Add new services under the business's provider
    public async Task EmployMember(ServiceProviderModel serviceProvider)
    {
        try
        {
            await _serviceProvider.CreateServiceProvider(serviceProvider);
        }
        catch (Exception ex)
        {
            serviceProvider = null;
            throw new Exception(ex.Message);
        }
    }


}
