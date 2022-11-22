using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query
{
    public class EmployeeData
    {
        ISQLDataAccess _dataAccess;

        public EmployeeData(ISQLDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        //Get an employee
        protected EmployeeModel GetEmployeeWithRatings(string EmployeeId)
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
                employee.ratings = new List<RatingsModel>()!;
                foreach (var er in ers)
                {

                    rating = new();
                    employee.employeeId = EmployeeId;
                    employee.BusinessId = er.emp_businessid;
                    rating.Id = er.rate_id;
                    rating.stars = er.rate_stars;
                    rating.review = er.rate_review;
                    rating.recommendation = er.rate_recommendation;


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
        protected void InsertEmployee(EmployeeModel employee)
        {
            try
            {
                //Insert the employee
                _dataAccess.SaveData("Delivery.spEmployeeInsert",
                       new
                       {
                           employeeId = employee.employeeId,
                           BusinessId = employee.BusinessId,
                           DateEmployed = employee.DateEmployed,
                       }, "Handyman_DB");

                //Insert the ratings
                foreach (var er in employee.ratings)
                {
                    int id = InsertRating(er);
                    _dataAccess.SaveData("Delivery.spEmployee_Rating_Insert",
                        new { providerId = employee.employeeId, ratingId = id }, "Handyman_DB");
                }

            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        //Helper method for rating insert
        int InsertRating(RatingsModel rating)
        {

            try
            {
                int rate_id = _dataAccess.LoadData<int, dynamic>("Delivery.spRatingInsert", rating, "Handyman_DB").First();

                return rate_id;
            }
            catch (Exception ex) { throw new Exception(); }
        }
        //Delete or remove employee
        protected virtual void Resign(string employeeId)
        {

        }
    }
}
