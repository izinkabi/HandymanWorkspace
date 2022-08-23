CREATE PROCEDURE [ServiceDelivery].[spProviderServiceLookUpByServiceId]
	--Get all the providers available for the given service 
    @ServiceId int
AS
BEGIN
	SELECT *
    FROM [ServiceDelivery].[ProviderService]
    WHERE ServiceId = @ServiceId
END
