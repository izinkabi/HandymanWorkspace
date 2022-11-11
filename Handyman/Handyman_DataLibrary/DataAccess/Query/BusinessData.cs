using Handyman_DataLibrary.DataAccess.Interfaces;
using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //Register a business
        public void Register(RegistrationModel registration)
        {

        }


        //Get the business of the loggedIn user
        public BusinessModel GetBusiness(string userId)
        {
            //Break the entities into models: business, employee and address
            //Build a business model
            //Return business Model

            //Get the provider as an employee
            EmployeeModel employee = _serviceProvider.GetEmployeeWithRatings(userId);

            //Get business, registration and address
            BusinessRegistrationModel businessRegistration = _dataAccess.LoadData<BusinessRegistrationModel, dynamic>("Delivery.spBusiness_Registration_LookUp", new { businessId = employee.BusinessId }, "Handyman_DB").First();

            //Populate the business
            BusinessModel business = new()!;

            business.Id = businessRegistration.bus_Id;
            business.date = businessRegistration.bus_datecreated;
            business.Employee = employee;
            //Populate registration
            business.registration.Id = businessRegistration.reg_Id;
            business.registration.name = businessRegistration.reg_name;
            business.registration.regNumber = businessRegistration.reg_regnumber;
            business.registration.taxNumber = businessRegistration.reg_taxnumber;

            //Populating address

            business.address.add_state = businessRegistration.add_state;
            business.address.add_country = businessRegistration.add_country;
            business.address.add_Id = businessRegistration.add_Id;
            business.address.add_zip = businessRegistration.add_zip;
            business.address.add_city = businessRegistration.add_city;
            business.address.add_latitude = businessRegistration.add_latitude;
            business.address.add_longitude = businessRegistration.add_longitude;


            return business;
        
        }

        

        public void EmployMember(EmployeeModel newEmployee)
        {
            //Call the _employeeData method for adding a new employee 
        }

        //Deleting the employee / Fire the nigga
        public void ReleaseEmployee()
        {
            //Also from _employeeData
        }

    }
}
