CREATE PROCEDURE [Service].[spPriceUpdate]
--Insert a negotiated-price (update the price table)
	@priceId int = 0,
	@negotiationPrice float 
AS
IF EXISTS (SELECT * FROM [price] WHERE [Id]=@priceId)
BEGIN
    UPDATE  [Service].[price] 
    SET [negotiated_price] = @negotiationPrice
END
