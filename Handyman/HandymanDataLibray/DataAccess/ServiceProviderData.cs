using HandymanDataLibrary.Internal.DataAccess;
using HandymanDataLibrary.Models;
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
        public ServiceProviderModel GetProviderById(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<ServiceProviderModel, dynamic>("dbo.spServiceProviderLookUp", p, "HandymanDB");

            return output.First();

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


       

    }
}
