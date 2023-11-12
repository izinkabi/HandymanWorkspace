CREATE PROCEDURE [Delivery].[spEmployeeInsert]
	
	 @employeeId NVARCHAR(450),
     @workShopId int = 0,
     @DateEmployed DATETIME2(7),
     @role int

AS
    IF NOT EXISTS(SELECT * FROM [Delivery].[employee] WHERE [emp_profile_id] = @employeeId)

    BEGIN 
	INSERT INTO [Delivery].[employee] (emp_workShopid,emp_date_employed,emp_profile_Id,emp_role)
    VALUES (@workShopId,@DateEmployed,@employeeId,@role)
    END
