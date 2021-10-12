using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ServiceEndPoint
    {
        private APIHelper _aPIHelper;
        private ServiceModel serviceModel;

        public ServiceEndPoint(APIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        public async Task<List<ServiceModel>> GetServices()
        {

            _aPIHelper = new APIHelper();
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/Services"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {


                    serviceModel = new ServiceModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<List<ServiceModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }

    }
}
