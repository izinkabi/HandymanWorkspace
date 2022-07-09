using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess
{
    public class ConsumerData
    {
        public ConsumerModel GetConsumerById(string Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            
            var p = new { ConsumerID = Id };
            
            var output = sql.LoadData<ConsumerModel, dynamic>("Customer.spGetConsumerById", p, "HandymanDB");

             return output.First();


        }

        //Post a consumer
        public void PostConsumer(ConsumerModel consumer)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData("Customer.spConsumerInsert", new { ConsumerID = consumer.Id,RegistrationDate = consumer.RegistrationDate}, "HandymanDB");
        }

    }
}
