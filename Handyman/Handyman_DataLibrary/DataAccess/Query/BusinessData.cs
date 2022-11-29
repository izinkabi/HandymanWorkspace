using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class BusinessData : IBusinessData
    {
        ISQLDataAccess _dataAccess;
        IServiceProviderData _serviceProvider;
        public BusinessData(ISQLDataAccess dataAccess, IServiceProviderData serviceProvider)
        {
            _dataAccess = dataAccess;
            _serviceProvider = serviceProvider;

        }

        //Register a business return registration business
        int Register(RegistrationModel registration)
        {
            int registrationId = 0;
            try
            {
                registrationId = _dataAccess.LoadData<int, dynamic>("Delivery.spRegistrationInsert",
                    new
                    {

                        reg_name = registration.name,
                        reg_regnumber = registration.regNumber,
                        reg_taxnumber = registration.taxNumber,
                        reg_businesstype = registration.businessType

                    }, "Handyman_DB").FirstOrDefault();
                return registrationId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //Add the business-address and return the address *-can move to address data-*
        int RegisterAddress(AddressModel address)
        {
            try
            {
                //Insert the address and get an ID
                var add_id = _dataAccess.SaveData("Delivery.spAddressInsert",

                    new
                    {
                        add_street = address.add_street,
                        add_suburb = address.add_suburb,
                        add_city = address.add_city,
                        add_zip = address.add_zip,
                        add_latitude = address.add_latitude,
                        add_longitude = address.add_longitude,
                        add_country = address.add_country,
                        add_state = address.add_state
                    },
                    "Handyman_DB");
                return add_id;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //Get the business of the loggedIn user
        public BusinessModel GetBusiness(string userId)
        {
            //Break the entities into models: business, employee and address
            //Build a business model
            //Return business Model

            //Get the provider as an employee
            ServiceProviderModel employee = _serviceProvider.GetServiceProvider(userId);

            //Get business, registration and address
            BusinessRegistrationModel businessRegistration = _dataAccess.LoadData<BusinessRegistrationModel, dynamic>("Delivery.spBusiness_Registration_LookUp",
                new { businessId = employee.BusinessId }, "Handyman_DB").FirstOrDefault();

            //Populating business
            BusinessModel business = new()!;

            business.Id = employee.BusinessId;
            business.date = businessRegistration.bus_datecreated;
            business.Type = businessRegistration.reg_businesstype;
            business.Name = businessRegistration.bus_name;

            //Employee

            business.Employee = new()!;
            business.Employee = employee;



            //Reistration
            business.registration = new()!;
            business.registration.Id = businessRegistration.reg_Id;
            business.registration.name = businessRegistration.reg_name;
            business.registration.regNumber = businessRegistration.reg_regnumber;
            business.registration.taxNumber = businessRegistration.reg_taxnumber;

            //Address
            business.address = new()!;
            business.address.add_state = businessRegistration.add_state;
            business.address.add_country = businessRegistration.add_country;
            business.address.add_Id = businessRegistration.add_Id;
            business.address.add_zip = businessRegistration.add_zip;
            business.address.add_city = businessRegistration.add_city;
            business.address.add_latitude = businessRegistration.add_latitude;
            business.address.add_longitude = businessRegistration.add_longitude;


            return business;

        }

        //Posting a business and its address
        //Returns an ID of the newly created business
        public int CreateBusiness(BusinessModel business)
        {
            int businessId = 0;
            try
            {

                if (business.registration != null && business.address != null)
                {
                    //Register and get an ID
                    int reg_id = Register(business.registration);

                    //Insert the address and get its id
                    int address_id = RegisterAddress(business.address);

                    _dataAccess.StartTransaction("Handyman_DB");
                    //Insert a new business and get an ID
                    businessId = _dataAccess.LoadDataTransaction<int, dynamic>("Delivery.spBusinessInsert",
                        new
                        {
                            //this is a new business (new address and registration)
                            bus_registrationid = reg_id,
                            bus_addressid = address_id,
                            bus_datecreated = business.date,
                            bus_name = business.Name

                        }).First();

                    _dataAccess.CommitTransation();

                    //then save the employee
                    if (business.Employee != null)
                    {
                        //Insert the employee
                        business.Employee.BusinessId = businessId;
                        EmployServiceProvider(business.Employee);
                    }

                    return businessId;
                }
            }
            catch (Exception ex)
            {
                _dataAccess.RollBackTransaction();
                throw new Exception(ex.Message);
            }
            return businessId;
        }

        //Create an employee in a business

        public void EmployServiceProvider(ServiceProviderModel serviceProvider)
        {
            try
            {
                //check if the service provider is a employee
                if (serviceProvider is not null)
                {
                    //Insert a provider and its employee details
                    _serviceProvider.InsertProvider(serviceProvider);

                    //Give the an employee a role 'Service Provider'

                    //_dataAccess.SaveData("spUserRoleInsert", new { userId = serviceProvider.pro_providerId, roleName = "ServiceProvider" }, "Handyman_DB");
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        //Deleting the employee / Fire the nigga
        public void ReleaseEmployee()
        {
            //Also from _employeeData
        }

    }
}
