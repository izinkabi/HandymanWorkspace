CREATE PROCEDURE [Delivery].[spEmployeeInsert]
	
	 @employeeId NVARCHAR(450),
     @BusinessId int = 0,
     @DateEmployed DATETIME2(7),
     @role int

AS
    IF NOT EXISTS(SELECT * FROM [Delivery].[employees] WHERE [emp_profile_id] = @employeeId)

    BEGIN 
	INSERT INTO [Delivery].[employees] (emp_businessid,emp_date_employed,emp_profile_Id,emp_role)
    VALUES (@BusinessId,@DateEmployed,@employeeId,@role)
    END
