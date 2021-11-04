
using HandymanDataLibrary.Models;
using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanDataLibray.DataAccess
{
    public class ServiceData
    {
        //Getting a service by Id
        public ServiceModel GetServiceById(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<ServiceModel, dynamic>("dbo.spServiceLookUp", p, "HandymanDB");

            return output.First();

        }

        /*Get Services in a list*/
        public List<ServiceModel> GetServices()
        {

            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ServiceModel, dynamic>("dbo.spServicesLookUp", new { }, "HandymanDB");
            return output;
        }

        /*Posting a new Service*/
        public void PostService(ServiceModel val)
        {
            SQLDataAccess sql = new SQLDataAccess();
            sql.SaveData<ServiceModel>("dbo.spServiceInsert", val, "HandymanDB");
        }


        public List<ServiceCategoryModel> GetServiceCategories()
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ServiceCategoryModel, dynamic>("dbo.spServiceCategoriesLookUp", new { }, "HandymanDB");
            return output;
        }
    }
}
