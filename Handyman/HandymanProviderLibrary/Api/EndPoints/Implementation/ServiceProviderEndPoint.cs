using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class ServiceProviderEndPoint : EmployeeEndPoint, IServiceProviderEndPoint
{

    IAPIHelper? _apiHelper;
    ServiceProviderModel? serviceProvider;

    public ServiceProviderEndPoint(IAPIHelper apiHelper) : base(apiHelper)
    {
        _apiHelper = apiHelper;
    }
    //Add service(s) of business's provider
    async Task IServiceProviderEndPoint.AddService(ServiceProviderModel provider)
    {
        try
        {
            await _apiHelper.ApiClient.PostAsJsonAsync("/api/Delivery/PostProviderService", provider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //Remove the service of a provider
    async Task IServiceProviderEndPoint.RemoveService(ServiceModel service, string providerId)
    {
        try
        {
            await _apiHelper.ApiClient.DeleteAsync($"/api/Delivery/Delete?Id={service.id}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    //Adding a new service provider
    async Task IServiceProviderEndPoint.CreateServiceProvider(ServiceProviderModel provider)
    {
        try
        {
            await _apiHelper.ApiClient.PostAsJsonAsync("/api/Delivery/AddMember", provider);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Create a new profile
    public async void CreateProfile(ProfileModel newProfile)
    {
        try
        {
            if (newProfile is null)
            {
                return;
            }
            await _apiHelper.ApiClient.PostAsJsonAsync("/api/handymen/PostProviderProfile", newProfile);
        }
        catch (Exception)
        {

            throw;
        }

    }

    //Query profile by Id
    public async Task<ProfileModel> GetProfile(string id)
    {
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                ProfileModel profile = await _apiHelper.ApiClient.GetFromJsonAsync<ProfileModel>($"/api/Handymen/GetProfile?userId={id}");
                if (profile != null) return profile;

            }
            return null;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
