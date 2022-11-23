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

            RatingsModel rating = new();
            try
            {
                //Get the profile of the service-provider
                _dataAccess.StartTransaction("Handyman_DB");
                employee.employeeProfile = new ProfileModel();
                employee.employeeProfile = _dataAccess.LoadDataTransaction<ProfileModel, dynamic>("dbo.spProfileLookUp", new { profileId = EmployeeId }).FirstOrDefault();

                //get the business , employee(with a profile) and the related ratings
                var ers = _dataAccess.LoadDataTransaction<Employee_Rating_Model, dynamic>("Delivery.spEmployeesLookUp", new { EmployeeId = EmployeeId });
                _dataAccess.CommitTransation();
                //popolate the data in the following sequence
                //Business
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
                _dataAccess.RollBackTransaction();
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

                _dataAccess.StartTransaction("Handyman_DB");

                _dataAccess.SaveDataTransaction("dbo.spProfileInsert", new
                {
                    //Start with employee as employee' details profile 
                    Names = employee.employeeProfile.Names,
                    Surname = employee.employeeProfile.Surname,
                    DateOfBirth = employee.employeeProfile.DateOfBirth.Date,
                    AddressId = employee.employeeProfile.AddressId,
                    PhoneNumber = employee.employeeProfile.PhoneNumber,
                    EmailAddress = employee.employeeProfile.EmailAddress,
                    profileGender = employee.employeeProfile.Gender,
                    userId = employee.employeeId


                });


                //Insert the employee
                _dataAccess.SaveDataTransaction("Delivery.spEmployeeInsert",
                       new
                       {
                           //Then complete the employee  
                           employeeId = employee.employeeId,
                           BusinessId = employee.BusinessId,
                           DateEmployed = DateTime.UtcNow
                       });

                _dataAccess.CommitTransation();

                //_dataAccess.SaveData("dbo.spUserRoleInsert", new { userId = employee.employeeId, RoleName = "ServiceProvider" }, "Handyman_DB");
            }
            catch (Exception ex)
            {
                _dataAccess.RollBackTransaction();
                throw new Exception(ex.Message);
            }
        }

        //Helper method for rating insert
        int InsertRating(RatingsModel rating)
        {

            try
            {
                int rate_id = _dataAccess.LoadData<int, dynamic>("Delivery.spRatingInsert", rating, "Handyman_DB").FirstOrDefault();

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
