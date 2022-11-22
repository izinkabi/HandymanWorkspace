CREATE PROCEDURE [Delivery].[spEmployeesLookUp]
	@EmployeeId NVARCHAR(450)
AS
Begin
	SELECT 
            e.[emp_businessid],e.[emp_date_employed],e.[emp_userid], 
            r.[rate_id],r.[rate_recommendation],r.[rate_review],r.[rate_stars],
            p.[Names],p.[Surname],p.[DateOfBirth],p.[Gender],p.[PhoneNumber],p.[AddressId]
  FROM 
          [Delivery].[employees] e , 
          [Delivery].[employee_ratings] c, 
          [Delivery].[ratings] r , 
          [dbo].[Profile] p
  
  WHERE 
          [e].[emp_userid] = @EmployeeId 
          AND [e].[emp_userid] = [p].[userid] 
          AND [r].[rate_id] = [c].[rating_id] 
END

