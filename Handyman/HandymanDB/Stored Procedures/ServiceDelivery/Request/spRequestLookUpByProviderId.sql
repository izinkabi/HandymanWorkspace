CREATE PROCEDURE [ServiceDelivery].[spRequestLookUpByProviderId]

	@ServiceProviderId NVARCHAR(450)
	
AS
BEGIN
	SELECT *
	FROM	[ServiceDelivery].[Request]
	WHERE	[ServiceProviderId] = @ServiceProviderId;
END
