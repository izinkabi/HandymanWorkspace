CREATE PROCEDURE [Delivery].[spOrderUpdate]
--Update the order by the request
	@requestId int = 0,
    @orderStatus int = 0
AS
BEGIN
	UPDATE [Delivery].[Order]
    SET [order_status] = @orderStatus
    WHERE [order_requestId] = @requestId
END
