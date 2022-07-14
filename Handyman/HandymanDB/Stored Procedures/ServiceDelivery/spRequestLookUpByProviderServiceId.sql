CREATE PROCEDURE [ServiceDelivery].[spProviderServiceLookUpByProviderId]
	@ServiceProviderId	NVARCHAR(128)
AS
BEGIN
Set nocount on
	SELECT ServiceProviderId,ServiceID,Id
	FROM [ServiceDelivery].[ProviderService] 
	WHERE ServiceProviderId = @ServiceProviderId;
END