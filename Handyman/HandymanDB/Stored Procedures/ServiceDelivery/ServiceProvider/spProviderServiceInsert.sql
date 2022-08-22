CREATE PROCEDURE [ServiceDelivery].[spProviderServiceInsert]
	@Id                INT,
    @ServiceProviderId NVARCHAR,
    @ServiceId             INT
AS
BEGIN
Set nocount on
	INSERT INTO [ServiceDelivery].[ProviderService] (Id,ServiceProviderId,ServiceId)
	VALUES (@Id,@ServiceProviderId,@ServiceId)	
END