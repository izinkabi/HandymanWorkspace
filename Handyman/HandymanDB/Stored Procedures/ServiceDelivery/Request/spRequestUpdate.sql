CREATE PROCEDURE [ServiceDelivery].[spRequestUpdate]
	@OrderId           INT,
    @ProviderServiceID INT,
    @IsDelivered      INT 
AS
BEGIN
	UPDATE	[ServiceDelivery].[Request]
	SET	 OrderId = @OrderId,ProviderServiceID = @ProviderServiceID,IsDelivered = IsDelivered
END
