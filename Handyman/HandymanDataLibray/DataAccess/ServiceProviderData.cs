using HandymanDataLibrary.Internal.DataAccess;
using HandymanDataLibrary.Models;
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
        public ServiceProviderModel GetProviderById(string UserId)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { UserId = UserId };

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
        public void PostProvider(ServiceProviderModel val)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData<ServiceProviderModel>("dbo.spServiceProviderInsert", val, "HandymanDB");
        }

        //Get provider's services by provider's ID
        public ProvidersServiceModel GetProvidersServicesById(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ProvidersServiceModel, dynamic>("dbo.spProvidersServiceLookUp", new { Id = Id}, "HandymanDB").First();
            return output;
        }

        //Put the ProvidersService
        public void PutProvidersService(ProvidersServiceModel providersServiceModel)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.SaveData<ProvidersServiceModel>("dbo.spProvidersServiceInsert", providersServiceModel, "HandymanDB");
        }
        //Getting Providers Services
        public List<ProvidersServiceModel> GetProvidersServices()
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ProvidersServiceModel, dynamic>("dbo.spProvidersServiceLookUp",new { }, "HandymanDB");
            return output;
        }

    }
}
