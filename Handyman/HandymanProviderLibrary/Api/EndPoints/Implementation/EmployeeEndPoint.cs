using HandymanProviderLibrary.API;
using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.Business.Implementation;

public class EmployeeEndPoint
{
    static IAPIHelper? _apiHelper;
    static EmployeeModel? employee;
    string? Error;
    static IList<RatingsModel>? ratings;
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
}
