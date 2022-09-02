using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess
{
    public class OrderData
    {
        public IEnumerable<OrderModel> GetOrdersByConsumerId(string Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
           

            var p = new { ConsumerId = Id };

            var output = sql.LoadData<OrderModel, dynamic>("Customer.OrderLookUpByConsumerID", p, "HandymanDB");

            return output;
        }

        
       

        //Post an Order
        public void PostOrder(OrderModel order)
        {
            SQLDataAccess sql = new SQLDataAccess();

            sql.SaveData("Customer.spOrderInsert", new
            {
                ConsumerID = order.ConsumerID,
                DateCreated = DateTime.Now,
                Stage = order.Stage,
                ServiceId = order.ServiceId//the ordered service because sql wont take a model inside a model
            }, "HandymanDB");
        }

        ////Updating the order
        public void UpdateOrder(OrderModel orderUpdate)
        {

            SQLDataAccess sql = new SQLDataAccess();

            /*var output = */sql.SaveData("dbo.spRequestUpdate", orderUpdate, "HandymanDB");

        }


        //public void DeleteOrder(int Id)
        //{
        //    SQLDataAccess sql = new SQLDataAccess();

        //    var output = sql.SaveData("Customer.spOrderDelete", new { Id = Id }, "HandymanDB");
        //}
        //}
    }
}
