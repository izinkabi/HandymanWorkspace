CREATE PROCEDURE [Delivery].[spEmployeesLookUp]
	@EmployeeId NVARCHAR(450)
AS
Begin
	SELECT e.[emp_businessid],e.[emp_date_employed],e.[emp_userid], 
            r.[rate_id],r.[rate_recommendation],r.[rate_review],r.[rate_stars]
  FROM [Delivery].[employees] e , [Delivery].[employee_ratings] c, [Handyman_DB].[Delivery].[ratings] r
  
  WHERE [e].[emp_userid] = @EmployeeId AND r.[rate_id]= c.rating_id
END

