CREATE PROCEDURE [ServiceDelivery].[spRequestDelete]
--Delete the request before the order
	@OrderId int = 0
	
AS
BEGIN
	DELETE FROM [ServiceDelivery].[Request]
    WHERE OrderId = @OrderId
END
