CREATE PROCEDURE [ServiceDelivery].[spProviderLookUpByProviderId]
	@ProviderId VARCHAR
AS
BEGIN
	SELECT *
    FROM [ServiceDelivery].[ProviderService]
    WHERE ServiceProviderId = @ProviderId
END
