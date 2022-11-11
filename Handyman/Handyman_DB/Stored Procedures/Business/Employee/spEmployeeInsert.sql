CREATE PROCEDURE [Delivery].[spEmployeeInsert]
	
	 @employeeId NVARCHAR(450),
     @ratingid int = 0,
     @BusinessId int = 0,
     @DateEmployed DATETIME2
AS
BEGIN
	INSERT INTO [Delivery].[employees] (emp_businessid ,emp_userid,emp_ratingid,emp_date_employed)
    VALUES (@BusinessId,@employeeId,@ratingid,@DateEmployed)

END
