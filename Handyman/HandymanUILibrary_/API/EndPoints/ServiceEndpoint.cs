using HandymanUILibrary_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary_.API.EndPoints
{
    public class ServiceEndpoint
    {
        private ApiHelper _api;
        public ServiceEndpoint()
        {

        }
        public ServiceEndpoint(ApiHelper api)
        {
            api = _api;
        }

       public async Task<List<ServiceModel>> GetAllServices()
        {
            using(HttpResponseMessage responseMessage = await _api.httpClient.GetAsync("/Services"))
            {
                if(responseMessage.IsSuccessStatusCode)
                {
                    var results = await responseMessage.Content.ReadFromJsonAsync<List<ServiceModel>>();
                    return results;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
    }
}
