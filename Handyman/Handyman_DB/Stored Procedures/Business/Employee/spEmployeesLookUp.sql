CREATE PROCEDURE [Delivery].[spEmployeesLookUp]
	@EmployeeId NVARCHAR(450)
AS
Begin
	SELECT 
            e.[emp_businessid],e.[emp_date_employed],e.[emp_userid], 
           
            p.[Names],p.[Surname],p.[DateOfBirth],p.[Gender],p.[PhoneNumber],p.[AddressId]
  FROM 
          [Delivery].[employees] e , 
          
         
          [dbo].[Profile] p
  
  WHERE 
          [e].[emp_userid] = @EmployeeId 
          AND [e].[emp_userid] = [p].[userid] 
          
END

