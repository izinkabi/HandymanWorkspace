CREATE PROCEDURE [ServiceDelivery].[sProviderServicesLookUp.sql]
	
AS
BEGIN
Set nocount on
	SELECT ServiceProviderId,JobID
	FROM [ServiceDelivery].[ProviderService] 
END