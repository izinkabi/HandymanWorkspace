using HandymanProviderLibrary.Api.ApiHelper;
using HandymanProviderLibrary.Models;

namespace HandymanProviderLibrary.Api.EndPoints.Implementation;

public class EmployeeEndPoint
{
    static IAPIHelper? _apiHelper;
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
}
