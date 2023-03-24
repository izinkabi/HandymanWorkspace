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
            //get the business , employee(with a profile) and the related ratings
            var er = _dataAccess.LoadData<Employee_Rating_Model, dynamic>("Delivery.spEmployeesLookUp", new { EmployeeId = EmployeeId }, "Handyman_DB").FirstOrDefault();
            if (er != null)
            {

                employee.employeeId = EmployeeId;
                employee.BusinessId = er.emp_businessid;
                employee.employeeProfile.Names = er.Names;
                employee.employeeProfile.Surname = er.Surname;
                employee.employeeProfile.PhoneNumber = er.PhoneNumber;
                employee.employeeProfile.DateOfBirth = er.DateOfBirth;

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

            //Insert the employee
            var result = _dataAccess.SaveData("Delivery.spEmployeeInsert",
                    new
                    {
                        //Then complete the employee  
                        employeeId = employee.employeeId,
                        BusinessId = employee.BusinessId,
                        DateEmployed = DateTime.UtcNow,
                        role = employee.IsOwner
                    },
                    "Handyman_DB");

        }
        catch (Exception ex)
        {

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

    //Get Memberships
    protected List<EmployeeModel> GetMemberships(int workShopID)
    {

        if (workShopID is not 0)
        {
            try
            {
                var ers = _dataAccess.LoadData<Employee_Rating_Model, dynamic>("Delivery.spEmployeesLookUpByWorkShop", new { workShopId = workShopID }, "Handyman_DB").DefaultIfEmpty();
                List<EmployeeModel> employees = new();
                foreach (var er in ers)
                {
                    if (er != null && er.emp_role > 0)
                    {
                        EmployeeModel employee = new();

                        employee.BusinessId = er.emp_businessid;
                        employee.employeeProfile.DateOfBirth = er.DateOfBirth;
                        employee.DateEmployed = er.emp_date_employed;
                        employee.employeeId = er.emp_profile_id;
                        employee.IsOwner = true;

                        employees.Add(employee);

                    }
                }
                return employees;
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message);
            }

        }
        return null;

    }
}
