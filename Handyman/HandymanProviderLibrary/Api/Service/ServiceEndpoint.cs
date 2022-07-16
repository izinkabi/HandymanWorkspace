using HandymanProviderLibrary.API;
using HandymanProviderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Api.Service;

public class ServiceEndpoint : IServiceEndpoint
{
    private IAPIHelper _aPIHelper;

    /// <summary>
    /// Constractor for the API helper
    /// </summary>
    /// <param name="aPIHelper"></param>
    /// 

    public ServiceEndpoint(IAPIHelper aPIHelper)
    {
        _aPIHelper = aPIHelper;
    }
    /// <summary>
    /// This method is used to Get a list of Service from the API
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<List<ServiceModel>> GetServices()
    {
        try
        {
            List<ServiceModel> httpResponseMessage = await _aPIHelper.ApiClient.GetFromJsonAsync<List<ServiceModel>>("Services/GetServies");
            return httpResponseMessage;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

}
