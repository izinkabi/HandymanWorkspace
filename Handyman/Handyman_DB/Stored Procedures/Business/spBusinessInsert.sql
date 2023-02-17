CREATE PROCEDURE [Delivery].[spBusinessInsert]
    @bus_registrationid INT = 0, 
    @bus_addressid INT = 0, 
    @bus_datecreated DATETIME2(7),
    @bus_name VARCHAR(150),
    @bus_description VARCHAR(400)

   
AS
BEGIN
	INSERT INTO [Delivery].[business] (bus_name,bus_addressid,bus_datecreated,bus_registrationid,bus_description)
    VALUES (@bus_name,@bus_addressid,@bus_datecreated,@bus_registrationid,@bus_description)

    DECLARE @Id INT = SCOPE_IDENTITY();

    SELECT [bus_Id] 
    FROM   [Delivery].[business] 
    WHERE  [bus_Id] = @Id
END
