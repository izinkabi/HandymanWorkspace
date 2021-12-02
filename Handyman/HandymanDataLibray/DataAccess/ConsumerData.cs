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
        public ConsumerModel GetConsumerByProfileId(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            ///*Getting Consumer by profileId*

            var p = new { ProfileId = Id };
            //    var p = new { Id = Id };

            var output = sql.LoadData<ConsumerModel, dynamic>("dbo.spConsumerLookUp", p, "HandymanDB");

             return output.First();


        }

        //Post the a consumer
        public void PostConsumer(ConsumerModel consumer)
        {           
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData("dbo.spConsumerInsert", new { ProfileId=consumer.ProfileId}, "HandymanDB");
        }

    }
}
