CREATE PROCEDURE [Service].[spPriceLookUp]
--Get the price of the id
	@priceId int = 0
AS
BEGIN
	SELECT * FROM [Service].[price]
    WHERE [Id] = @priceId
END
