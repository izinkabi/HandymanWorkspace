CREATE PROCEDURE [Request].[spDeleteRequestTask]
--Delete the Order_Task records on the condition of requestId and consumerId 
	@requestId int = 0,
    @consumerId NVARCHAR(450)

AS
BEGIN


--Delete bridge table records
    DELETE FROM [Delivery].[order]
    WHERE [order_requestId] = @requestId

    DELETE  FROM [Request].[request_task]
    WHERE [request_id] = @requestId 

--Delete the order,
	DELETE  FROM [Request].[request]
    WHERE [req_id] = @requestId AND [req_consumer_id] = @consumerId

END
RETURN 0
