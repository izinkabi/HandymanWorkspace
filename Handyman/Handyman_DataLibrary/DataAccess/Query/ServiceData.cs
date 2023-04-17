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

        IList<Service_CategoryModel> output = _dataAccess.LoadData<Service_CategoryModel, dynamic>(
            "Service.spServiceLookUp_GroupByCategory",
            new { },
            "Handyman_DB");

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

            //Get a list of custom services
            service.Customs = GetCustomServices(service.id);

            services.Add(service);
        }

        return services;


    }

    //Get a Custom Service
    private List<CustomServiceModel> GetCustomServices(int oId)
    {
        try
        {
            List<CustomServiceModel>? customServices = _dataAccess.LoadData<CustomServiceModel, dynamic>(
                "Service.spCustomServicesLookUp",
                new { serviceId = oId },
                "Handyman_DB");

            //Check if the list is no empty
            if (customServices != null && customServices.Count() > 0)
            {
                if (customServices.Any())
                {
                    return customServices;
                }

            }

            return null;
        }
        catch (Exception)
        {

            throw;
        }
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
                service.PriceId = serviceCat.price_id;


                service.category = new ServiceCategoryModel();

                //populate category
                service.category.name = serviceCat.cat_name;
                service.category.description = serviceCat.cat_description;
                service.category.type = serviceCat.cat_type;

                //populate customs
                service.Customs = GetCustomServices(service.id);
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
                     new { orderId = id }, "Handyman_DB").FirstOrDefault();

                if (serviceCat != null)
                {
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

                }

                return service;

            }

            return service;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message, ex.InnerException);
        }
    }

    //Insert services  using a transaction since it might have multiple customs
    public void InsertServices(List<ServiceModel> services)
    {
        try
        {
            _dataAccess.StartTransaction("Handyman_DB");
            foreach (ServiceModel service in services)
            {
                //Save the new service
                _dataAccess.SaveDataTransaction<ServiceModel>("Service.spServiceInsert", service);
                //save a custom of a service
                if (service.Customs != null && service.Customs.Count > 0)
                {
                    foreach (var custom in service.Customs)
                    {
                        _dataAccess.SaveDataTransaction("Service.spCustomService_Insert", custom);
                    }
                }
            }
            _dataAccess.CommitTransation();
        }
        catch (Exception)
        {
            _dataAccess.RollBackTransaction();
            throw;
        }
    }

    //Update the service OR insert customs
    //Customs only insert if the service exists
    public int InsertCustomService(ServiceModel serviceUpdate)
    {
        try
        {
            if (serviceUpdate == null || serviceUpdate.Customs.Count == 0)
            {
                return 0;
            }
            int customServiceId = 0;
            //Insert custom services of updated service
            foreach (var custom in serviceUpdate.Customs)
            {
                if (custom != null)
                {
                    customServiceId = _dataAccess.LoadData<int, dynamic>("Service.spCustomService_Insert", custom, "Handyman_DB").First();
                }

            }

            return customServiceId;

        }
        catch (Exception ex)
        {
            return 0;
            throw new Exception(ex.Message);
        }
    }

    //Insert the negotiated price of the service
    //as it might be handy in getting a general price for each service
    public bool InsertNegotiatedPrice(int priceId, float nPrice)
    {
        try
        {
            var result = _dataAccess.SaveData("Service.spPriceUpdate", new { priceId = priceId, negotiationPrice = nPrice }, "Handyman_DB");
            return true;
        }
        catch (Exception ex)
        {
            return false;
            throw new Exception(ex.Message, ex.InnerException);
        }

    }

    //Update the service
    public void UpdateService(ServiceModel serviceUpdate)
    {
        try
        {
            var result = _dataAccess.SaveData<ServiceModel>("Service.spServiceUpdate", serviceUpdate, "Handyman_DB");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Insert a new base-price and get its id
    public int InsertBasePrice(float price)
    {
        try
        {
            int priceId = _dataAccess.SaveData("Service.spPriceInsert", new { basePrice = price }, "Handyman_DB");
            return priceId;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }

    }

    //Get the price of the given id
    public PriceModel GetPrice(int priceId)
    {
        try
        {
            var price = _dataAccess.LoadData<PriceModel, dynamic>("Service.spPriceLookUp", new { priceId = priceId }, "Handyman_DB").FirstOrDefault();
            return price;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    //Get workshop services
    public List<CustomServiceModel> GetWorkShopServices(int workShopRegId)
    {
        try
        {
            List<CustomServiceModel> workShopServices = _dataAccess.LoadData<CustomServiceModel, dynamic>("Delivery.spWorShopServices_LookUp", new { workShopRegId = workShopRegId }, "Handyman_DB");
            return workShopServices;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            return null;
        }
    }

    //Update workshop service
    public void UpdateWorkshopService(CustomServiceModel wsService)
    {
        try
        {
            if (wsService is null || wsService is not CustomServiceModel)
            {
                return;
            }

            _dataAccess.SaveData<CustomServiceModel>("Delivery.spWorkShopService_Update", wsService, "Handyman_DB");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    //Delete a service of a workshop 
    public void DeleteService(int wsServiceId, int wsRegId)
    {
        if (wsServiceId == 0 || wsRegId == 0)
        {
            return;
        }
        try
        {
            _dataAccess.SaveData("Delivery.spWorkShopService_Delete", new
            {
                workShopServiceId = wsServiceId,
                workShopRegId = wsRegId
            }, "Handyman_DB");

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    public void DeleteProviderService(int wsServiceId, string providerId)
    {
        try
        {
            _dataAccess.SaveData("Delivery.spProviderService_Delete", new
            {
                ServiceId = wsServiceId,
                pro_providerId = providerId
            }, "Handyman_DB");
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
