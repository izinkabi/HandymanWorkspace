CREATE PROCEDURE [Delivery].[spEmployeeInsert]
	
	 @employeeId NVARCHAR(450),
     @BusinessId int = 0,
     @DateEmployed DATETIME2(7)

AS
    IF NOT EXISTS(SELECT * FROM [Delivery].[employees] WHERE [emp_userid] = @employeeId)

    BEGIN 
	INSERT INTO [Delivery].[employees] (emp_businessid ,emp_userid, emp_date_employed, emp_profile_Id)
    VALUES (@BusinessId,@employeeId,@DateEmployed,@employeeId)
    END
