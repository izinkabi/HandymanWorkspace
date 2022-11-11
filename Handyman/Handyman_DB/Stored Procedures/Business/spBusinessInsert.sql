CREATE PROCEDURE [Delivery].[spBusinessInsert]
    @bus_registrationid INT = 0, 
    @bus_addressid INT = 0, 
    @bus_datecreated DATETIME2
AS
BEGIN
	INSERT INTO [Delivery].[business] (bus_addressid,bus_datecreated,bus_registrationid)
    VALUES (@bus_addressid,@bus_datecreated,@bus_registrationid)

    DECLARE @Id INT = SCOPE_IDENTITY();
    SELECT [bus_Id] 
    FROM   [Delivery].[business] 
    WHERE  [bus_Id] = @Id
END
