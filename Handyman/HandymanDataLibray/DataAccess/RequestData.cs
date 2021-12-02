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

        public RequestModel GetRequestByConsumerId(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();
            ///*Getting request by Id*

            var p = new { Id = Id };
            //    var p = new { Id = Id };

            var output = sql.LoadData<RequestModel, dynamic>("dbo.spRequestLookUp", p, "HandymanDB");

            return output.First();


        }

        //Post the a request
        public void PostRequest(RequestModel request)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData<RequestModel>("dbo.spRequestInsert", request, "HandymanDB");
        }

    }
}
