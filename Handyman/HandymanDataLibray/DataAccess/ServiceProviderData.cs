using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess
{
    public class ServiceProviderData
    {

        //Getting the service provider by Id
        public ServiceProviderModel GetProviderById(string Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.LoadData<ServiceProviderModel, dynamic>("ServiceDelivery.spGetServiceProviderById",new { Id = Id }, "HandymanDB").FirstOrDefault();

            return output;

        }

        /*Getting Service providers in a list*/
        //public List<ServiceProviderModel> GetServiceProviders()
        //{
            
        //    SQLDataAccess sql = new SQLDataAccess();
        //    var output = sql.LoadData<ServiceProviderModel, dynamic>("dbo.spServiceProvidersLookUp", new { }, "HandymanDB");
        //    return output;
        //}

        /*Posting a new Service Provider*/
        /*The following method will eventually be converted to a transaction as it should post the service provider
         followed by the provider's service(s)*/
        public void PostProvider(ServiceProviderModel provider)
        {
            var p = new {Id = provider.Id,RegistrationDate = provider.RegistrationDate,CompanyName = provider.CompanyName,ProviderType = provider.ProviderType };
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData("ServiceDelivery.spServiceProviderInsert", provider, "HandymanDB");
        }

        

        //Put the ProvidersService
        public void PutProvidersService(string providerId,int jobId)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var providersService = new {JobId = jobId, ServiceProviderId = providerId};
            var output = sql.SaveData("dbo.spProvidersServiceInsert", providersService, "HandymanDB");
        }
        //Getting Providers Services
        public List<ProviderServiceModel> GetProvidersServiceByProviderId(string providerId)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ProviderServiceModel, dynamic>("ServiceDelivery.spGetProviderServiceByProviderId",new { ServiceProviderId = providerId}, "HandymanDB");
            return output;
        }

        public void UpdateServiceProvider(ServiceProviderModel provider)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.SaveData("ServiceDelivery.spServiceProviderUpdate", new { Id = provider.Id,CompanyName = provider.CompanyName, ProviderType = provider.ProviderType}, "HandymanDB");
        }

        public void DeleteProvidersService(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.SaveData("dbo.spProvidersServiceDelete", new { Id = Id}, "HandymanDB");
        }

    }
}
