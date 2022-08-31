using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess
{
    public class RequestData
    {
        //Get the relevant orders for the provider to accept or decline
        public List<OrderModel> GetAllOrdersByService(int serviceId)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.LoadData<OrderModel, dynamic>("ServiceDelivery.spOrdersLookUpByServiceId", new { ServiceId = serviceId }, "HandymanDB");

            return output;
        }

        ////Get a list of request by provider's id
        public List<RequestModel> GetRequestsByProviderId(string providerId)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.LoadData<RequestModel, dynamic>("dbo.spRequestLookUpByProviderId", new { Id = providerId }, "HandymanDB");

            return output;
        }


        ////Post a request
        public void PostRequest(RequestModel request)
        {
            var req = new
            {
                ProviderServiceID = request.ProviderServiceID,
                IsDelivered = request.IsDelivered,
                OrderId = request.OrderId
            };
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData("dbo.spRequestInsert", req, "HandymanDB");
        }


        ////Updating the request
        public void UpdateRequest(RequestModel update)
        {
            var reqUpdate = new
            {
                ProviderServiceID = update.ProviderServiceID,
                IsDelivered = update.IsDelivered,
                OrderId = update.OrderId

            };
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.SaveData("dbo.spRequestUpdate", reqUpdate, "HandymanDB");

        }


        public void DeleteRequest(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.SaveData("dbo.spRequestDelete", new { Id = Id }, "HandymanDB");
        }
    }
}
