using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class ServiceProviderData : EmployeeData , IServiceProviderData
    {
        ISQLDataAccess _dataAccess;

        public ServiceProviderData(ISQLDataAccess dataAccess):base(dataAccess) 
        {
            _dataAccess = dataAccess;
        }

        //public ServiceProviderData(ISQLDataAccess dataAccess) 
        //{
        //    _dataAccess = dataAccess;
        //}

        public void InsertServices(ServiceProviderModel provider)
        {
            foreach (var service in provider.ratings)
            {

            }
        }

        //Remove a service of a provider
        //public void RemoveService(int serviceId, string providerId)
        //{

        //}

        //Get the service provider and the services
        public ServiceProviderModel GetServiceProvider(string providerId)
        {
            try
            {
                //Get the service of the provider
                List<Service_CategoryModel> service_category = _dataAccess.LoadData<Service_CategoryModel, dynamic>("spProvider_Service_LookUp", 
                    new { provider = providerId }, "Handyman_DB");

                var services = new List<ServiceModel>();
                var provider = new ServiceProviderModel()!;

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
                return provider;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
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
        }

        //protected override void Resign(string employeeId)
        //{
        //    base.Resign(employeeId);
        //}
    }
}
