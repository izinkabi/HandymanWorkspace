CREATE PROCEDURE [Delivery].[spEmployeeInsert]
	
	 @employeeId NVARCHAR(450),
     @BusinessId int = 0,
     @DateEmployed DATETIME2(7)
AS
BEGIN
	INSERT INTO [Delivery].[employees] (emp_businessid ,emp_userid,emp_date_employed)
    VALUES (@BusinessId,@employeeId,@DateEmployed)

END
