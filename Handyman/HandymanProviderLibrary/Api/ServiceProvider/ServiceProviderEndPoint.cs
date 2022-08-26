using HandymanProviderLibrary.API;
using HandymanProviderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanProviderLibrary.Api.ServiceProvider
{
    public class ServiceProviderEndPoint : IServiceProviderEndPoint
    {

        private IAPIHelper _apiHelper;


        public ServiceProviderEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }



        //Get the provider's by service Id
        public async Task<List<ProviderServiceModel>> GetProvidersByServiceId(int serviceId)
        {
            try
            {
                List<ProviderServiceModel>? httpResponseMessage = await _apiHelper.ApiClient
                    .GetFromJsonAsync<List<ProviderServiceModel>>($"api/GetProvidersServicesByServiceId?serviceId={serviceId}");
                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
