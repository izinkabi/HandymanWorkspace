CREATE PROCEDURE [Service].[spPriceInsert]
--Inserting a new price for the service 
	@basePrice float
AS
BEGIN

	INSERT INTO [Service].[price] ([base_price])
    VALUES (@basePrice)
    DECLARE @Id INT = SCOPE_IDENTITY();
END
RETURN @Id

