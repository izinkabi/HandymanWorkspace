using SP_MLibrary.Models;
using SP_MLibrary.Services.ServiceHelper;
using System.Net.Http.Json;

namespace SP_MLibrary.Services.Implementation;

public class EmployeeEndPoint
{
    IServiceHelper _ServicesHelper;
    EmployeeModel employee;
    string Error;
    IList<RatingsModel> ratings;
    public EmployeeEndPoint(IServiceHelper ServicesHelper)
    {
        _ServicesHelper = ServicesHelper;
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

    //Get a Service provider of the given ID
    public async Task<MemberModel> GetProvider(string userId)
    {
        try
        {
            if (userId != null)
            {
                var serviceProvider = await _ServicesHelper.ServicesClient.GetFromJsonAsync<MemberModel>($"/Services/Delivery/GetProvider?employeeId={userId}");
                if (serviceProvider != null)
                {
                    return serviceProvider;
                }

            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }

    }
}
