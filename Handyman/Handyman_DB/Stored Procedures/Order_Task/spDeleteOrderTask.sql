CREATE PROCEDURE [Request].[spDeleteOrderTask]
	@OrderId int = 0,
    @consumerId NVARCHAR(450)

AS
BEGIN


--Delete bridge table records
    DELETE FROM [Delivery].[request]
    WHERE [req_orderid] = @orderId

    DELETE  FROM [Request].[order_task]
    WHERE [order_id] = @orderId 

--Delete the order,
	DELETE  FROM [Request].[order]
    WHERE [ord_id] = @orderId AND [ord_consumer_id] = @consumerId

END
RETURN 0
