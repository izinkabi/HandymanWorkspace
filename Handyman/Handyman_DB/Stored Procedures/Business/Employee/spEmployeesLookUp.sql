CREATE PROCEDURE [Delivery].[spEmployeesLookUp]
	@EmployeeId NVARCHAR(450)
AS
Begin
	SELECT 
            e.[emp_businessid],e.[emp_date_employed],e.[emp_profile_id], e.[emp_role],
           
            p.[Names],p.[Surname],p.[DateOfBirth],p.[Gender],p.[PhoneNumber],p.[AddressId]
  FROM 
          [Delivery].[employees] e, 
          [dbo].[Profile] p
  
  WHERE 
           [p].[userid] = [e].[emp_profile_id] AND
           [e].[emp_profile_id] = @EmployeeId 
          
END

