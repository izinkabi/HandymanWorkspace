using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class RegisterProviderEndPoint
    {

        private APIHelper _aPIHelper;
        private ServiceProviderModel providerModel;
        //Initializing
        public RegisterProviderEndPoint(APIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        //a Profile Post endpoint
        public async Task<ServiceProviderModel> PostServiceProvider(ServiceProviderModel serviceProviderModel)
        {
            using (HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/ServiceProvider", serviceProviderModel))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<ServiceProviderModel>();
                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }

        }

        //Getting a provider endpoint
        public async Task<ServiceProviderModel> GetProviderById(string userId)
        {

            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/ServiceProviders"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {


                    providerModel = new ServiceProviderModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<List<ServiceProviderModel>>();

                    foreach (ServiceProviderModel sp in result)
                    {
                        if (sp.UserId==userId)
                        {
                            return sp;
                        }
                    }

                    //providerModel.UserId = result.UserId;
                    //providerModel.Name = result.Name;

                    //providerModel.Surname = result.Surname;
                    //providerModel.DateOfBirth = result.DateOfBirth;
                    //providerModel.HomeAddress = result.HomeAddress;
                    //providerModel.PhoneNumber = result.PhoneNumber;

                    return providerModel;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

        }

        //Get a list of providers
        public async Task<List<ServiceProviderModel>> GetServiceProviders()
        {

            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/ServiceProviders"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {


                    providerModel = new ServiceProviderModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<List<ServiceProviderModel>>();
              
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
