CREATE PROCEDURE [ServiceDelivery].[spRequestUpdate]
	@OrderId           INT,
    @ServiceProviderId NVARCHAR(450),
    @IsDelivered      INT ,
    @Status VARCHAR(100)
AS
BEGIN
	UPDATE	[ServiceDelivery].[Request]
	SET	 OrderId = @OrderId, ServiceProviderId = @ServiceProviderId,IsDelivered = @IsDelivered,Status = @Status
    WHERE ServiceProviderId = @ServiceProviderId AND OrderId = @OrderId

END
