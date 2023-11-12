CREATE PROCEDURE [Delivery].[MemberInsert]
--Insert a new Workshop record
    @workShop_registrationid INT = 0, 
    @workShop_addressid INT = 0, 
    @workShop_datecreated DATETIME2(7),
    @workShop_name VARCHAR(150),
    @workShop_description VARCHAR(400)

   
AS
BEGIN
	INSERT INTO [Delivery].[workshop] (work_name,work_addressid,work_datecreated,work_registrationid,work_description)
    VALUES (@workShop_name,@workShop_addressid,@workShop_datecreated,@workShop_registrationid,@workShop_description)

    DECLARE @Id INT = SCOPE_IDENTITY();

    SELECT [work_Id] 
    FROM   [Delivery].[workshop] 
    WHERE  [work_Id] = @Id
END
