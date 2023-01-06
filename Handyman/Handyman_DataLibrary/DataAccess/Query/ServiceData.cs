using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;


namespace Handyman_DataLibrary.DataAccess.Query;

public class ServiceData : IServiceData
{
    ISQLDataAccess _dataAccess;
    public ServiceData(ISQLDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    //Get service, by sp query specifications
    public List<ServiceModel> GetAllServices()
    {

        IList<Service_CategoryModel> output = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Service.spServiceLookUp_GroupByCategory", new { }, "Handyman_DB");

        //populate the service and its category
        var services = new List<ServiceModel>();

        foreach (Service_CategoryModel outputItem in output)
        {
            //populate service
            var service = new ServiceModel()!;
            service.datecreated = outputItem.serv_datecreated;
            service.status = outputItem.serv_status;
            service.img = outputItem.serv_img;
            service.id = outputItem.serv_id;
            service.name = outputItem.serv_name;
            service.category = new ServiceCategoryModel();

            //populate category
            service.category.name = outputItem.cat_name;
            service.category.description = outputItem.cat_description;
            service.category.type = outputItem.cat_type;
            services.Add(service);
        }

        return services;

    }
}
