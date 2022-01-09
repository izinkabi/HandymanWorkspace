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
            var req = new
            {
                ConsumerId = request.ConsumerId,
                ProvidersServiceId = request.ProvidersServiceId,
                RequestStatus = request.RequestStatus,
                StartTime = request.StartTime
            };
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData("dbo.spRequestInsert", req, "HandymanDB");
        }


        //Updating the request
        public void UpdateRequest(RequestModel update)
        {
            var reqUpdate = new
            {
                //only these attributes are updated, if more is to be done then we'll delete the request for security reasons
                Id = update.Id,             
                RequestStatus = update.RequestStatus,
                FinishTime = update.FinishTime
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
