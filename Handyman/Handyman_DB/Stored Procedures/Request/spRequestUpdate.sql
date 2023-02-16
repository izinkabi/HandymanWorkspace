CREATE PROCEDURE [Request].[spRequestUpdate]
	@orderId int = 0,
    @requestStatus int = 0
AS
BEGIN
	UPDATE [Delivery].[request]
    SET [req_status] = @requestStatus
    WHERE [req_orderid] = @orderId
END
