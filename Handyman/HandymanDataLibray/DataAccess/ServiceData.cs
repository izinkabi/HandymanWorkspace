using HandymanDataLibray.DataAccess.Internal;
using HandymanDataLibray.Models;
using System.Collections.Generic;


namespace HandymanDataLibray.DataAccess
{
    public class ServiceData
    {
        public List<ServiceModel> GetAllServices()
        {
            SQLDataAccess sql = new SQLDataAccess();

            var output = sql.LoadData<ServiceModel, dynamic>("Service.spServicesLookUp", new { }, "HandymanDB");

            return output;

        }

        //Get Jobs by Category Id
        public List<ServiceModel> GetServicesByCategoryId(int Id)
        {
            SQLDataAccess sql = new SQLDataAccess();

            var p = new { CategoryID = Id };

            var output = sql.LoadData<ServiceModel, dynamic>("Service.spServiceLookUpByCategoryId", p, "HandymanDB");

            return output;

        }

        //Posting a new Job
        public void PostService(ServiceModel service)
        {
            SQLDataAccess sql = new SQLDataAccess();
            
            sql.SaveData("Service.spServiceInsert", service, "HandymanDB");
        }

        //Get all Job Categories
        public List<ServiceCategoryModel> GetServiceCategories()
        {
            SQLDataAccess sql = new SQLDataAccess();
            var output = sql.LoadData<ServiceCategoryModel, dynamic>("Service.spCategoriesLookUp", new { }, "HandymanDB");
            return output;
        }
    }
}
