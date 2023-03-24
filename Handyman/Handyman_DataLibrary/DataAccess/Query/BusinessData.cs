using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class BusinessData : IBusinessData
    {
        ISQLDataAccess? _dataAccess;
        IServiceProviderData? _serviceProvider;
        ServiceProviderModel employee;
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

        //Get Business Id
        public BusinessModel GetBusinessById(int? busId)
        {
            try
            {
                if (busId != null && busId.Value != 0)
                {

                    BusinessRegistrationModel businessRegistration = _dataAccess.LoadData<BusinessRegistrationModel, dynamic>("Delivery.spBusiness_Registration_LookUp",
                    new { businessId = busId }, "Handyman_DB").FirstOrDefault();

                    //Populating business
                    BusinessModel business = new()!;

                    business.Id = businessRegistration.bus_Id;
                    business.date = businessRegistration.bus_datecreated;
                    business.Type = businessRegistration.reg_businesstype;
                    business.Name = businessRegistration.bus_name;
                    business.Description = businessRegistration.bus_description;

                    //Reistration

                    business.registration.Id = businessRegistration.reg_Id;
                    business.registration.name = businessRegistration.reg_name;
                    business.registration.regNumber = businessRegistration.reg_regnumber;
                    business.registration.taxNumber = businessRegistration.reg_taxnumber;

                    //Address
                    business.address.add_state = businessRegistration.add_state;
                    business.address.add_zip = business.address.add_zip;
                    business.address.add_Id = businessRegistration.add_Id;
                    business.address.add_country = businessRegistration.add_country;
                    business.address.add_suburb = businessRegistration.add_suburb;
                    business.address.add_street = businessRegistration.add_street;

                    if (business != null) return business;

                }

            }
            catch (Exception)
            {

                throw;
            }

            return null;
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

        //Posting a business and its address
        //Returns an ID of the newly created business
        public BusinessModel CreateBusiness(BusinessModel business)
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
                            bus_name = business.Name,
                            bus_description = business.Description

                        }).First();

                    _dataAccess.CommitTransation();

                    //then save the employee
                    if (business.Employees != null && business.Employees.Count > 0)
                    {
                        foreach (var employee in business.Employees)
                        {
                            //Insert the employee
                            employee.BusinessId = businessId;
                            EmployServiceProvider(employee);
                        }

                    }

                    return business;
                }
            }
            catch (Exception ex)
            {
                _dataAccess.RollBackTransaction();
                throw new Exception(ex.Message);
            }
            return business;
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

        //Get the workshop by registration number
        public BusinessModel GetWorkShop(string regNumber)
        {
            try
            {

                BusinessRegistrationModel workshop = _dataAccess.LoadData<BusinessRegistrationModel, dynamic>(
                    "Delivery.spWorkShopsLookUp",
                new { regNumber = regNumber }, "Handyman_DB").First();

                if (workshop != null)
                {
                    //Populating business
                    BusinessModel business = new()!;

                    business.Id = workshop.bus_Id;
                    business.date = workshop.bus_datecreated;
                    business.Type = workshop.reg_businesstype;
                    business.Name = workshop.bus_name;
                    business.Description = workshop.bus_description;

                    //Reistration

                    business.registration.Id = workshop.reg_Id;
                    business.registration.name = workshop.reg_name;
                    business.registration.regNumber = workshop.reg_regnumber;
                    business.registration.taxNumber = workshop.reg_taxnumber;

                    //Address
                    business.address.add_state = workshop.add_state;
                    business.address.add_zip = business.address.add_zip;
                    business.address.add_Id = workshop.add_Id;
                    business.address.add_country = workshop.add_country;
                    business.address.add_suburb = workshop.add_suburb;
                    business.address.add_street = workshop.add_street;


                    //Get employees for each workshop
                    business.Employees = _serviceProvider.GetMemberShips(business.Id);

                    return business;

                }
                else
                {
                    return null;
                }


            }
            catch (Exception)
            {
                throw;
                return null;
            }

        }

        //Deleting the employee / Fire the nigga
        public void ReleaseEmployee()
        {
            //Also from _employeeData
        }

        //Get a service provider of the given employee ID
        public ServiceProviderModel GetProvider(string employeeId)
        {
            try
            {
                ServiceProviderModel sp = _serviceProvider.GetServiceProvider(employeeId);
                return sp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void AddProviderServices(ServiceProviderModel provider)
        {
            try
            {
                if (provider != null && provider.pro_providerId != null)
                {
                    if (provider.BusinessId == 0)
                    {
                        return;
                    }
                    else
                    {
                        _serviceProvider.InsertProvider(provider);
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
