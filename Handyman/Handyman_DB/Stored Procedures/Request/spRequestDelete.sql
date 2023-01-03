CREATE PROCEDURE [Request].[spRequestDelete]
	@orderId int = 0
AS
BEGIN
	DELETE [Delivery].[request]
    WHERE [req_orderId] =@orderId
END
