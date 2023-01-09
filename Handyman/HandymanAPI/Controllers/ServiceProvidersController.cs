using HandymanDataLibray.DataAccess;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace HandymanAPI.Controllers
{
    
    public class ServiceProvidersController : ApiController
    {

       
        private ServiceProviderData providerData;

        //GET: api/GetProvidersServices
        //Getting the provider's service by service id
        [Route("api/GetProvidersServicesByProviderId")]
        public List<ProviderServiceModel> GetProvidersServicesByProviderId(string providerId)
        {
            providerData = new ServiceProviderData();
            try
            {


                var providerService = providerData.GetProvidersServiceByProviderId(providerId);
                return providerService;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get all the providers of this service id
        [Route("api/GetProvidersServicesByServiceId")]
        public List<ProviderServiceModel> GetProvidersServicesByServiceId(int serviceId)
        {
            providerData = new ServiceProviderData();
            try
            {

                var providerService = providerData.GetProvidersServiceByServiceId(serviceId);
                return providerService;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        //Create the provider's service
        [Route("api/PostProvidersService")]
        public void Post(ProviderServiceModel providersServiceModel)
        {
            providerData = new ServiceProviderData();
            try
            {
                providerData.PutProvidersService(providersServiceModel.ServiceProviderId, providersServiceModel.ServiceId);

            } catch (SqlException ex)
            {
                if(ex.Errors.Equals(2627))//Duplicate primary key
                {
                    return;
                }
                
            }
        }



        // DELETE: api/ProvidersService/5
        [Route("api/DeleteProvidersService")]
        public void DeleteProvidersService(int id)
        {
            try
            {
                providerData = new ServiceProviderData();
                providerData.DeleteProvidersService(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }

        }
    }
}
