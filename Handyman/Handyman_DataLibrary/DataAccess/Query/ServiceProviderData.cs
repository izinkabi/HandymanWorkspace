using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query;

public class ServiceProviderData : EmployeeData, IServiceProviderData
{
    ISQLDataAccess? _dataAccess;
    ServiceProviderModel? providerLocalmodel;
    public ServiceProviderData(ISQLDataAccess dataAccess) : base(dataAccess)
    {
        _dataAccess = dataAccess;
    }



    //Remove a service of a provider
    public void RemoveService(int serviceId, string providerId)
    {

    }

    public List<ServiceProviderModel> GetMemberShips(int businessId)
    {
        try
        {
            List<ServiceProviderModel> members = new List<ServiceProviderModel>();
            if (businessId != 0)
            {

                //Get employee
                List<EmployeeModel> employees = this.GetMemberships(businessId);


                //Get the service of the provider (Provider's Service)
                foreach (var employee in employees)
                {
                    List<Service_CategoryModel> service_category = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Delivery.spProvider_Service_LookUp",
                    new { providerId = employee.employeeId }, "Handyman_DB");


                    var provider = new ServiceProviderModel()!;
                    //Membership
                    provider.IsOwner = employee.IsOwner;
                    provider.DateEmployed = employee.DateEmployed;
                    provider.BusinessId = employee.BusinessId;

                    //Services
                    var services = new List<ServiceModel>();
                    foreach (Service_CategoryModel outputItem in service_category)
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

                    provider.Services = services;
                    members.Add(provider);
                }


            }
            else
            {
                return null;
            }
            return members;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    //Since the service provider is an employee, get the employee then
    //Get the service provider and the services
    public ServiceProviderModel GetServiceProvider(string providerId)
    {
        try
        {

            if (providerId != null)
            {    //Get employee
                var employee = this.GetEmployeeWithRatings(providerId);


                //Get the service of the provider
                List<Service_CategoryModel> service_category = _dataAccess.LoadData<Service_CategoryModel, dynamic>("Delivery.spProvider_Service_LookUp",
                    new { providerId = providerId }, "Handyman_DB");


                var provider = new ServiceProviderModel()!;
                provider.BusinessId = employee.BusinessId;
                provider.employeeId = employee.employeeId;
                provider.pro_providerId = employee.employeeId;
                provider.DateEmployed = employee.DateEmployed;
                provider.employeeProfile = employee.employeeProfile;


                var services = new List<ServiceModel>();
                foreach (Service_CategoryModel outputItem in service_category)
                {
                    //populate service
                    var service = new ServiceModel()!;
                    service.datecreated = outputItem.serv_datecreated;
                    service.status = outputItem.serv_status;
                    service.img = outputItem.serv_img;
                    service.id = outputItem.serv_id;
                    service.name = outputItem.serv_name;
                    service.category = new ServiceCategoryModel();
                    service.PriceId = outputItem.price_id;

                    //populate category
                    service.category.name = outputItem.cat_name;
                    service.category.description = outputItem.cat_description;
                    service.category.type = outputItem.cat_type;
                    services.Add(service);
                }

                provider.Services = services;
                providerLocalmodel = provider;
                return provider;
            }

            return providerLocalmodel;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //Insert the employee followed by provider 
    public void InsertProvider(ServiceProviderModel serviceProvider)
    {
        try
        {
            //insert the employee details 
            if (serviceProvider.employeeId != null && serviceProvider.employeeProfile.UserId != null)
            {
                this.InsertEmployee(serviceProvider);
            }

            if (serviceProvider.Services != null && serviceProvider.Services.Count() > 0)
            {
                foreach (var service in serviceProvider.Services)
                {
                    //Insert the provider 
                    _dataAccess.SaveData("Delivery.spProviderInsert",
                        new
                        {
                            pro_providerId = serviceProvider.pro_providerId,
                            ServiceId = service.id
                        },
                        "Handyman_DB");
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
