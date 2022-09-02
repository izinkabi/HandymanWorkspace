CREATE PROCEDURE [ServiceDelivery].[spProviderLookUpByProviderId]
	@ProviderId NVARCHAR(450)
AS
BEGIN
	SELECT *
    FROM [ServiceDelivery].[ProviderService]
    WHERE ServiceProviderId = @ProviderId
END
