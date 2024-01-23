CREATE PROCEDURE [Delivery].[spOrderDelete]
--Remve the order 
	@requestId int = 0
AS
BEGIN
	DELETE [Delivery].[order]
    WHERE [order_id] =@requestId
END
