CREATE PROCEDURE [ServiceDelivery].[spProviderServiceInsert]
    @ServiceProviderId NVARCHAR(450),
    @ServiceId   INT
AS
BEGIN
Set nocount on
	INSERT INTO [ServiceDelivery].[ProviderService] (ServiceProviderId,ServiceId)
	VALUES (@ServiceProviderId,@ServiceId)	
END