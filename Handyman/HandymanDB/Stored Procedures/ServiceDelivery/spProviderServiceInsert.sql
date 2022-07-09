CREATE PROCEDURE [ServiceDelivery].[spProviderServiceInsert]
	@Id                INT,
    @ServiceProviderId NVARCHAR,
    @JobId             INT
AS
BEGIN
Set nocount on
	INSERT INTO [ServiceDelivery].[ProviderService] (Id,ServiceProviderId,JobId)
	VALUES (@Id,@ServiceProviderId,@JobId)	
END