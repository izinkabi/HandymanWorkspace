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

            var output = sql.LoadData<RequestModel, dynamic>("ServiceDelivery.spRequestLookUpByProviderId", new { ServiceProviderId = providerId }, "HandymanDB");

            return output;
        }


        ////Post a request
        public void PostRequest(RequestModel request)
        {
            var req = new
            {
                ServiceProviderId = request.ProviderId,
                IsDelivered = request.IsDelivered,
                OrderId = request.OrderId,
                ServiceId = request.ServiceId,
                DateAccepted = DateTime.Now
            };
            SQLDataAccess sql = new SQLDataAccess();
           
            try
            {
                //Create request and update order
                sql.StartTransaction("HandymanDB");
                sql.SaveDataTransaction("ServiceDelivery.spRequestInsert", req);

                var order = sql.LoadDataTransaction<OrderModel, dynamic>("ServiceDelivery.spOrderLookUpById", new { Id  = request.OrderId }).FirstOrDefault();

                //let the consumer know the orderis accepted
                var orderUpdate = new
                {
                    Id = order.Id,
                    Stage = "Started",
                    ServiceId = order.ServiceId,
                    DateAccepted = DateTime.Now,
                    IsAccepted = 1
                };
                sql.SaveDataTransaction("Customer.spOrderUpdate", orderUpdate);

                sql.CommitTransation();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        ////Updating the request
        public void UpdateRequest(RequestModel update)
        {
            var reqUpdate = new
            {
                ServiceProviderId = update.ProviderId,
                IsDelivered = update.IsDelivered,
                OrderId = update.OrderId,
                ServiceId = update.ServiceId,
                Status = update.Status


            };
            SQLDataAccess sql = new SQLDataAccess();

            try
            {
                //Create request and update order
                sql.StartTransaction("HandymanDB");
                sql.SaveDataTransaction("ServiceDelivery.spRequestUpdate", reqUpdate);

                var order = sql.LoadDataTransaction<OrderModel, dynamic>("ServiceDelivery.spOrderLookUpById", new { Id = reqUpdate.OrderId }).FirstOrDefault();

                //let the consumer know the orderis accepted
                var orderUpdate = new
                {
                    Id = order.Id,
                    Stage = "Started",
                    ServiceId = order.ServiceId,
                    DateAccepted = DateTime.Now,
                    IsAccepted = 1
                };
                sql.SaveDataTransaction("Customer.spOrderUpdate", orderUpdate);

                sql.CommitTransation();

                //var output = sql.SaveData("ServiceDelivery.spRequestUpdate", reqUpdate, "HandymanDB");

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void DeleteRequest(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.SaveData("dbo.spRequestDelete", new { Id = Id }, "HandymanDB");
        }
    }
}
