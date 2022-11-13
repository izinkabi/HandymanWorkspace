using HandymanProviderLibrary.API;
using HandymanProviderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Api.Business.Implementation;

public class EmployeeEndPoint
{
    static IAPIHelper? _apiHelper;
    EmployeeModel? employee = new()!;
    string? Error;
    static IList<RatingsModel>? ratings;
    public EmployeeEndPoint(IAPIHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

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
