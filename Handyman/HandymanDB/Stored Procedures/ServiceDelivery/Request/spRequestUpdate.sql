CREATE PROCEDURE [ServiceDelivery].[spRequestUpdate]
	@OrderId           INT,
    @ServiceProviderId NVARCHAR(450),
    @IsDelivered      INT 
AS
BEGIN
	UPDATE	[ServiceDelivery].[Request]
	SET	 OrderId = @OrderId, ServiceProviderId = @ServiceProviderId,IsDelivered = IsDelivered
END
