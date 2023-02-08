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

    /// <summary>
    /// Get service, by sp query specifications
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Getting a service by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public ServiceModel GetService(int id)
    {
        try
        {
            ServiceModel service = new ServiceModel();
            if (id != 0)
            {
                Service_CategoryModel serviceCat = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUpBy_Id",
                     new { serviceId = id }, "Handyman_DB").First();
                //Populate service
                service.name = serviceCat.serv_name;
                service.status = serviceCat.serv_status;
                service.datecreated = serviceCat.serv_datecreated;
                service.img = serviceCat.serv_img;
                service.id = serviceCat.serv_id;


                service.category = new ServiceCategoryModel();

                //populate category
                service.category.name = serviceCat.cat_name;
                service.category.description = serviceCat.cat_description;
                service.category.type = serviceCat.cat_type;

                return service;

            }

            return service;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    /// <summary>
    /// Looking for a service of the given order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public ServiceModel GetServiceByOrder(int id)
    {
        try
        {
            ServiceModel service = new ServiceModel();
            if (id != 0)
            {
                Service_CategoryModel serviceCat = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Request.spServiceLookUp_ByOrder",
                     new { orderId = id }, "Handyman_DB").First();

                //Populate service
                service.name = serviceCat.serv_name;
                service.status = serviceCat.serv_status;
                service.datecreated = serviceCat.serv_datecreated;
                service.img = serviceCat.serv_img;
                service.id = serviceCat.serv_id;


                service.category = new ServiceCategoryModel();

                //populate category
                service.category.name = serviceCat.cat_name;
                service.category.description = serviceCat.cat_description;
                service.category.type = serviceCat.cat_type;

                return service;

            }

            return service;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
