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
            _aPIHelper = new APIHelper();
            using (HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/ServiceProviders", serviceProviderModel))
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
        public async Task<ServiceProviderModel> GetProviderByUserId(ServiceProviderModel sp)
        {
            //ServiceProviderModel serviceProvider = new ServiceProviderModel();
            _aPIHelper = new APIHelper();
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/GetServiceProviderById"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {


                    providerModel = new ServiceProviderModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<ServiceProviderModel>();

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
        public async Task<ServiceProviderModel> GetServiceProviders(ServiceProviderModel serviceProvider)
        {

            if (_aPIHelper != null)
            {


                //_aPIHelper = new APIHelper();
                using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/GetServiceProviders"))
                {
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {


                        providerModel = new ServiceProviderModel();

                        var result = await httpResponseMessage.Content.ReadAsAsync<List<ServiceProviderModel>>();

                        foreach (ServiceProviderModel sp in result)
                        {
                            if (sp.UserId == serviceProvider.UserId)
                            {
                                providerModel = sp;
                            }

                        }
                       
                    }
                    else
                    {
                        throw new Exception(httpResponseMessage.ReasonPhrase);
                    }
                }
            }
            return providerModel;

        }


        public async Task<List<ProvidersServiceModel>> GetProvidersServices()
        {
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/ProvidersServices"))
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {

                    providerModel = new ServiceProviderModel();

                    var result = await httpResponseMessage.Content.ReadAsAsync<List<ProvidersServiceModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
        }
        

        public async Task<ProvidersServiceModel> PostProvidersService(ProvidersServiceModel providersService)
        {
            ProvidersServiceModel ps = new ProvidersServiceModel();
            if (_aPIHelper != null)
            {

                using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.PutAsJsonAsync("/api/PutProvidersService", providersService))
                {
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {


                        ps = await httpResponseMessage.Content.ReadAsAsync<ProvidersServiceModel>();
                        
                    }
                    else
                    {
                        throw new Exception(httpResponseMessage.ReasonPhrase);
                    }
                }
            }
            return ps;
        }

    }
}
