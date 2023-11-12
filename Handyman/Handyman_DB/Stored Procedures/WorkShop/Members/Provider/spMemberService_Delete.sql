CREATE PROCEDURE [Delivery].[spProviderService_Delete]
--Delete provider's service if it exists
	@ServiceId INT = 0,
    @member_profileId NVARCHAR(450)
AS
IF EXISTS (SELECT * FROM [Delivery].[member] 
WHERE [member_serviceid]=@ServiceId AND [member_profileId] = @member_profileId)
BEGIN
	DELETE FROM [Delivery].[member] 
    WHERE [member_serviceid]=@ServiceId AND [member_profileId] = @member_profileId
END
