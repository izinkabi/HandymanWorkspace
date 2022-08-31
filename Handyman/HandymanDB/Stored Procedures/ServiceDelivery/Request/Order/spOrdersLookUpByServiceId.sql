CREATE PROCEDURE [ServiceDelivery].[spOrdersLookUpByServiceId]
		--Get all the orders of this service
        @ServiceId int
AS
BEGIN
	SELECT *  
	FROM [Customer].[Order]
	WHERE ServiceId = @ServiceId
END