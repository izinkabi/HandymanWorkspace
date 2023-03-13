using Handyman_DataLibrary.Internal.DataAccess;
using Handyman_DataLibrary.Models;

namespace Handyman_DataLibrary.DataAccess.Query;

public class EmployeeData
{
    ISQLDataAccess? _dataAccess;

    public EmployeeData(ISQLDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
    //Get an employee
    protected EmployeeModel GetEmployeeWithRatings(string EmployeeId)
    {
        EmployeeModel employee = new();


        try
        {
            //Get the profile of the service-provider
            _dataAccess.StartTransaction("Handyman_DB");
            employee.employeeProfile = _dataAccess.LoadDataTransaction<ProfileModel, dynamic>("dbo.spProfileLookUp", new { userId = EmployeeId }).FirstOrDefault();

            //get the business , employee(with a profile) and the related ratings
            var er = _dataAccess.LoadDataTransaction<Employee_Rating_Model, dynamic>("Delivery.spEmployeesLookUp", new { EmployeeId = EmployeeId }).FirstOrDefault();
            if (er != null)
            {
                _dataAccess.CommitTransation();
                employee.employeeId = EmployeeId;
                employee.BusinessId = er.emp_businessid;
                if (er.emp_role == 0)
                {
                    employee.IsOwner = false;
                }
                else
                {
                    employee.IsOwner = true;
                }
            }

        }
        catch (Exception ex)
        {
            _dataAccess.RollBackTransaction();
            employee = null;
            throw new Exception(ex.Message, ex.InnerException);
        }
        return employee;
    }

    //Create a new employee
    protected void InsertEmployee(EmployeeModel employee)
    {
        try
        {

            _dataAccess.StartTransaction("Handyman_DB");

            //Insert the employee
            _dataAccess.SaveDataTransaction("Delivery.spEmployeeInsert",
                   new
                   {
                       //Then complete the employee  
                       employeeId = employee.employeeId,
                       BusinessId = employee.BusinessId,
                       DateEmployed = DateTime.UtcNow,
                       role = employee.IsOwner
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
