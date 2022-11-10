CREATE PROCEDURE [Delivery].[spEmployeesLookUp]
	@EmployeeId NVARCHAR(450)
AS
Begin
	SELECT *
  FROM [Handyman_DB].[Delivery].[employees] e
  inner join [Handyman_DB].[Delivery].[ratings] r ON [e].[emp_ratingid] = [r].[rate_id]
  
  WHERE [e].[emp_businessid] = @EmployeeId
END

