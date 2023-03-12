using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Models;
using System.Net.Http.Json;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class EmployeeEndPoint
{
    IAPIHelper? _apiHelper;
    EmployeeModel? employee;
    string? Error;
    IList<RatingsModel>? ratings;
    public EmployeeEndPoint(IAPIHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    public EmployeeModel Employee { get => _ = employee; }

    //Get the loggedIn Employee
    protected async Task SignContract()
    {

    }


    //Update profile
    protected async Task UpdateProfile()
    {

    }

    //Leave the business hence stop working
    protected async Task Resign()
    {

    }

    //Get a service provider of the given ID
    public async Task<ServiceProviderModel> GetProvider(string userId)
    {


        try
        {
            if (userId != null)
            {
                var serviceProvider = await _apiHelper.ApiClient.GetFromJsonAsync<ServiceProviderModel>($"/api/Delivery/Business/GetProvider?employeeId={userId}");
                return serviceProvider;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }

    }
}
