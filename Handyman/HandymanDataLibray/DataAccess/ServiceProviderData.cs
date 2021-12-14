
using HandymanDataLibrary.Models;
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
        public ServiceProviderModel GetProviderByProfileId(int profileId)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { ProfileId = profileId };

            var output = sql.LoadData<ServiceProviderModel, dynamic>("dbo.spServiceProviderLookUp", p, "HandymanDB").FirstOrDefault();

            return output;

        }

        /*Getting Service providers in a list*/

        public List<ServiceProviderModel> GetServiceProviders()
        {
            
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ServiceProviderModel, dynamic>("dbo.spServiceProvidersLookUp", new { }, "HandymanDB");
            return output;
        }

        /*Posting a new Service Provider*/
        /*The following method will eventually be converted to a transaction as it should post the service provider
         followed by the provider's service(s)*/
        public void PostProvider(ServiceProviderModel val)
        {
            var provider = new {ProfileId = val.ProfileId,CompanyName = val.CompanyName,ProviderType = val.ProviderType };
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData("dbo.spServiceProviderInsert", provider, "HandymanDB");
        }

        

        //Put the ProvidersService
        public void PutProvidersService(ProvidersServiceModel providersServiceModel)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.SaveData<ProvidersServiceModel>("dbo.spProvidersServiceInsert", providersServiceModel, "HandymanDB");
        }
        //Getting Providers Services
        public List<ProvidersServiceModel> GetProvidersServiceByProviderId(int providerId)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ProvidersServiceModel, dynamic>("dbo.spProvidersServiceLookUp",new { ServiceProviderId = providerId}, "HandymanDB");
            return output;
        }

        public void UpdateServiceProvider(ServiceProviderModel provider)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.SaveData("dbo.spServiceProviderUpdate", new { CompanyName = provider.CompanyName,ProfileId = provider.ProfileId, ProviderType = provider.ProviderType}, "HandymanDB");
        }

        public void DeleteProvidersService(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.SaveData("dbo.spProvidersServiceDelete", new { Id = Id}, "HandymanDB");
        }

    }
}
