CREATE PROCEDURE [ServiceDelivery].[sProviderServicesLookUp.sql]
	
AS
BEGIN
Set nocount on
	SELECT ServiceProviderId,ServiceId
	FROM [ServiceDelivery].[ProviderService] 
END