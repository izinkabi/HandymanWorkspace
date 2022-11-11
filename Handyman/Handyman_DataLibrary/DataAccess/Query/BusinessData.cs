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
        public BusinessRegistrationModel GetBusiness(string userId)
        {
            //Get the stored-procedure that returns a business, registration and address
            BusinessModel business = _dataAccess.LoadData<BusinessRegistrationModel, dynamic>("spBusiness_Registration_LookUp", new { businessId = businessId }, "Handyman_DB").First();

            EmployeeModel employee =  _serviceProvider.GetEmployeeWithRatings(userId);
            business.Employee = new();
            
            //Populating


            return business;
            //Break the entities into models
            //Build a business model
            //Return business Model
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
