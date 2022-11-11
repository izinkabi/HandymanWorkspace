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
    public class EmployeeData : IEmployeeData
    {
        ISQLDataAccess _dataAccess;

        public EmployeeData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        //Get an employee
        public EmployeeModel GetEmployeeWithRatings(string EmployeeId)
        {
            EmployeeModel employee = new();
            /// List<RatingsModel> ratings = new();
            RatingsModel rating = new();
            try
            {
                //get the business , employee and the related ratings
                var ers = _dataAccess.LoadData<Employee_Rating_Model, dynamic>("Delivery.spEmployeesLookUp", new { EmployeeId = EmployeeId }, "Handyman_DB");

                //popolate the data in the following sequence
                //Employee
                //Rating
                foreach (var er in ers)
                {
                    employee.employeeId = er.emp_Id;

                    rating = new();
                    rating.Id = er.rate_id;
                    rating.stars = er.rate_stars;
                    rating.review = er.rate_review;
                    rating.recommnedation = er.rate_recommendation;
                    employee.ratings.Add(rating);


                }
            }
            catch (Exception)
            {
                employee = null;
                throw;
            }
            return employee;
        }
        
        //Create a new employee
        public void InsertEmployee(EmployeeModel employee)
        {
            try
            {
                _dataAccess.SaveData<EmployeeModel>("Delivery.spEmployeeInsert", employee, "Handyman_DB");
            }catch(Exception ex) { throw new Exception(ex.Message); }
        }

        //Delete or remove employee
        protected virtual void Resign(string employeeId)
        {

        }
    }
}
