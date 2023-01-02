CREATE PROCEDURE [Delivery].[spOrderUpdateByRequest]
	@orderId int = 0,
	@status NCHAR(100)
AS
BEGIN
	UPDATE [Request].[order]
    SET
    [ord_status] = @status
    WHERE [ord_id] = @orderId

END
