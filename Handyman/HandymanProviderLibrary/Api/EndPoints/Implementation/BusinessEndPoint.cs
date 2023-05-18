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
    public async Task<BusinessModel> GetBusiness(int busId)
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
            if (business != null)
            {
                var response = await _apiHelper.ApiClient.PostAsJsonAsync<BusinessModel>("/api/Delivery/Create", business);
                BusinessModel newBusiness;
                if (response.IsSuccessStatusCode)
                {
                    newBusiness = response.Content.ReadFromJsonAsync<BusinessModel>().Result;
                    return newBusiness;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    //Add new services under the business's provider
    public async Task<bool> EmployMember(ServiceProviderModel serviceProvider)
    {
        try
        {
            if (serviceProvider != null)
            {
                var result = await _serviceProvider.CreateServiceProvider(serviceProvider);
                return result;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            serviceProvider = null;
            return false;
            throw new Exception(ex.Message);
        }
    }



    public async Task<BusinessModel> GetWorkShop(string regNumber)
    {
        try
        {
            var WorkShop = await _apiHelper.ApiClient.GetFromJsonAsync<BusinessModel>($"/api/Delivery/GetWorkShop?regNumber={regNumber}");
            return WorkShop;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    public async Task<bool> InsertWorkShopService(int workShopRegId, int customServiceId)
    {
        if (workShopRegId == 0 || customServiceId == 0)
        {
            return false;
        }

        try
        {
            var result = await _apiHelper.ApiClient.PostAsJsonAsync($"/api/delivery/InsertWorkShopService?workshopRegId={workShopRegId}&&customServiceId={customServiceId}", new { });
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
            return false;
            throw new Exception(ex.Message);
        }
    }
}
