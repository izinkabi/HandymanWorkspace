using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Api.EndPoints.Interface;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class ServiceProviderEndPoint : EmployeeEndPoint, IServiceProviderEndPoint
{

    private readonly IAPIHelper? _apiHelper;
    private readonly AuthenticatedUserModel _authedUser;
    ServiceProviderModel? serviceProvider;

    public ServiceProviderEndPoint(IAPIHelper apiHelper, AuthenticatedUserModel authedUserModel) : base(apiHelper)
    {
        _apiHelper = apiHelper;
        _authedUser = authedUserModel;
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
    async Task<bool> IServiceProviderEndPoint.CreateServiceProvider(ServiceProviderModel provider)
    {
        try
        {

            if (provider != null)
            {

                if (provider.pro_providerId is not null)
                {
                    HttpResponseMessage responseMessage = new HttpResponseMessage();
                    responseMessage = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Delivery/AddNewMember", provider);
                    return responseMessage.IsSuccessStatusCode;
                }
                else
                {
                    return false;
                }
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

    //Create a new profile
    public async Task<bool> CreateProfile(ProfileModel newProfile)
    {
        try
        {
            if (newProfile is null)
            {
                return false;
            }

            if (_authedUser != null && _authedUser.Access_Token != null || newProfile.UserId != null)
            {
                if (newProfile.UserId != null && string.IsNullOrEmpty(_authedUser.Access_Token))
                {
                    _authedUser.Access_Token = await _apiHelper.AuthenticateUser(newProfile.UserId);
                }

                if (!string.IsNullOrEmpty(_authedUser.Access_Token))
                {
                    HttpResponseMessage responseMessage = new HttpResponseMessage();
                    _apiHelper.InitializeClient(_authedUser.Access_Token);
                    responseMessage = await _apiHelper.ApiClient.PostAsJsonAsync<ProfileModel>("/api/auth/PostProfile", newProfile);
                    return responseMessage.IsSuccessStatusCode;
                }
                else
                {
                    return false;
                }

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
