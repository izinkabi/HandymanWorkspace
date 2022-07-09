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
        public List<OrderModel> GetOrdersByConsumerId(string Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            ///*Getting Job by Id*

            var p = new { ConsumerId = Id };

            List<OrderModel> output = sql.LoadData<OrderModel, dynamic>("Customer.spConsumerById", p, "HandymanDB");

            return output;
        }

        //Get the Job Offers assigned to a service provider Id
       

        //Post an Order
        public void PostOrder(OrderModel order)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData("ServiceDelivery.spRequestInsert", order, "HandymanDB");
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
