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
        //Get a list of request by the consumer's id
        public List<RequestModel> GetRequestsByConsumerId(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            ///*Getting requests by Id*

            var p = new { Id = Id };
            //    var p = new { Id = Id };

            var output = sql.LoadData<RequestModel, dynamic>("dbo.spRequestLookUp", p, "HandymanDB");

            return output;


        }


        //Post a request
        public void PostRequest(RequestModel request)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData<RequestModel>("dbo.spRequestInsert", request, "HandymanDB");
        }


        //Updating the request
        public void UpdateRequest(RequestModel update)
        {
            SQLDataAccess sql = new SQLDataAccess();
           
            var output = sql.SaveData<RequestModel>("dbo.spRequestLookUp", update, "HandymanDB");

        }


        public void DeleteRequest(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.SaveData("dbo.spRequestDelete", new { Id = Id }, "HandymanDB");
        }
    }
}
