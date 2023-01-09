using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System.Collections.Generic;


namespace HandymanDataLibray.DataAccess
{
        //This a service provider data class 
    public class ServiceProviderData
    {

        //Update the Provider's Service
        public void PutProvidersService(string providerId,int serviceId)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var providersService = new {ServiceId = serviceId, ServiceProviderId = providerId};
            var output = sql.SaveData("ServiceDelivery.spProviderServiceInsert", providersService, "HandymanDB");
        }
        //Getting Provider's Services By the provider ID
        public List<ProviderServiceModel> GetProvidersServiceByProviderId(string providerId)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ProviderServiceModel, dynamic>("ServiceDelivery.spProviderLookUpByProviderId",new { ProviderId = providerId}, "HandymanDB");
            return output;
        }
        //Get the provider's service by the service ID
        public List<ProviderServiceModel> GetProvidersServiceByServiceId(int serviceId)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ProviderServiceModel, dynamic>("ServiceDelivery.spProviderServiceLookUpByServiceId", new { ServiceId = serviceId }, "HandymanDB");
            return output;
        }
        //Remove / Achieve the provider's service
        public void DeleteProvidersService(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.SaveData("dbo.spProvidersServiceDelete", new { Id = Id}, "HandymanDB");
        }

    }
}
