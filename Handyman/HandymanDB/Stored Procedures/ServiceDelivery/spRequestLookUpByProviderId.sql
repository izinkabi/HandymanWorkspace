CREATE PROCEDURE [ServiceDelivery].[spRequestLookUpByProviderServiceId]
	@ProviderServiceID int = 0
	
AS
BEGIN
	SELECT *
	FROM	[ServiceDelivery].[Request]
	WHERE	ProviderServiceID = @ProviderServiceID;
END
