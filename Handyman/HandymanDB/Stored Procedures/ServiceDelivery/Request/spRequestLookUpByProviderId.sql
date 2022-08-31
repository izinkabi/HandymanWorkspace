CREATE PROCEDURE [ServiceDelivery].[spRequestLookUpByProviderServiceId]

	@ServiceProviderId NVARCHAR(450)
	
AS
BEGIN
	SELECT *
	FROM	[ServiceDelivery].[Request]
	WHERE	[ServiceProviderId] = @ServiceProviderId;
END
