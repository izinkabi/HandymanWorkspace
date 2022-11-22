using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class ServiceProviderData : EmployeeData, IServiceProviderData
    {
        ISQLDataAccess _dataAccess;

        public ServiceProviderData(ISQLDataAccess dataAccess) : base(dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public void InsertServices(ServiceProviderModel provider)
        {
            foreach (var service in provider.ratings)
            {

            }
        }

        //Remove a service of a provider
        public void RemoveService(int serviceId, string providerId)
        {

        }

        //Since the service provider is an employee, get the employee then
        //Get the service provider and the services
        public ServiceProviderModel GetServiceProvider(string providerId)
        {
            try
            {

                //Get employee
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

                    //populate category
                    service.category.name = outputItem.cat_name;
                    service.category.description = outputItem.cat_description;
                    service.category.type = outputItem.cat_type;
                    services.Add(service);
                }

                provider.Services = services;
                provider.ratings = employee.ratings;

                return provider;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
<<<<<<< HEAD
        
        //public int InsertProvider(ServiceProviderModel serviceProvider)
        //{

        //}

        public void RemoveService(int serviceId, string providerId)
        {
            throw new NotImplementedException();
        }

        public int InsertProvider(ServiceProviderModel serviceProvider)
        {
            throw new NotImplementedException();
=======

        //Insert the employee followed by provider 
        public void InsertProvider(ServiceProviderModel serviceProvider)
        {
            try
            {
                //insert the employee details 
                this.InsertEmployee(serviceProvider);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
>>>>>>> 1576c75f23d5518700009eba9f6c9919e7494c91
        }


    }
}
