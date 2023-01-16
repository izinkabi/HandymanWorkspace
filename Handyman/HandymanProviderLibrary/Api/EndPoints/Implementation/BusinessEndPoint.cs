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
    public async Task<BusinessModel>? GetLoggedInEmployee(string employeeid)
    {
        try
        {
            business = new();
            if (employeeid != null)
                business = await _apiHelper?.ApiClient?.GetFromJsonAsync<BusinessModel>($"/api/Delivery/Get?employeeid={employeeid}");

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return business;
    }

    //Create a new business
    public async Task CreateNewBusiness(BusinessModel business)
    {
        try
        {
            await _apiHelper.ApiClient.PostAsJsonAsync("/api/Delivery/Business/Create", business);
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
