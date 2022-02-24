using HandymanUILibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandymanUILibrary.API
{
    public class ServiceProviderEndPoint: IServiceProviderEndPoint
    {

        private IAPIHelper _aPIHelper;
        private ServiceProviderModel providerModel;
        //Initializing
        public ServiceProviderEndPoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        //a Profile Post endpoint
        public async Task<ServiceProviderModel> PostServiceProvider(ServiceProviderModel serviceProviderModel)
        {
           
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

        //Getting a provider by profileId
        public async Task<ServiceProviderModel> GetProviderByProfileId(int profileId)
        {
            providerModel = new ServiceProviderModel();

            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetServiceProviderByProfileId?ProfileId={profileId}"))//passing the profileId to return the ServiceProvider
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    try
                    {

                        var result = await httpResponseMessage.Content.ReadAsAsync<ServiceProviderModel>();
                        providerModel.ProviderType = result.ProviderType;
                        providerModel.ProfileId = result.ProfileId;
                        providerModel.Id = result.Id;
                        providerModel.CompanyName = result.CompanyName;
                        return providerModel;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
               
            }
            return providerModel;

        }

        //Get a list of providers
        public async Task<List<ServiceProviderModel>> GetServiceProviders()
        {
            List<ServiceProviderModel> providers = new List<ServiceProviderModel>();
            if (_aPIHelper != null)
            {

                
                //_aPIHelper = new APIHelper();
                using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync("/api/GetServiceProviders"))
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

            return providers;
        }

        //Get a list of service for the given service provider's id
        public async Task<List<ProvidersServiceModel>> GetProvidersServiceByProviderId(int providerId)
        {
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.GetAsync($"/api/GetProvidersServiceByProviderId?ProviderId={providerId}"))
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



        public async Task<ServiceProviderModel> UpdateServiceProvider(ServiceProviderModel serviceProvider)
        {
            using (HttpResponseMessage responseMessage = await _aPIHelper.ApiClient.PostAsJsonAsync("/api/UpdateServiceProvider", serviceProvider))
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


        //Delete a provider's service
        public async Task DeleteProvidersService(int Id)
        {
            using (HttpResponseMessage httpResponseMessage = await _aPIHelper.ApiClient.DeleteAsync($"/api/DeleteProvidersService?Id={Id}"))//Deleting the service of the provider under the given provider's profile id and service id at hand 
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                   var  result = await httpResponseMessage.Content.ReadAsAsync<ProvidersServiceModel>();
                }
                else
                {
                    throw new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
        }
    }
}
