CREATE PROCEDURE [Request].[spServicePriceNegotiationInsert]
--updating the service price as per negotiation
	@negitiatedPrice float,
	@priceId int
AS
BEGIN
	UPDATE [Service].[price] 
    SET [negotiated_price] = @negitiatedPrice
    WHERE [Id] = @priceId
END
