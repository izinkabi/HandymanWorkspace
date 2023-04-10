CREATE PROCEDURE [Delivery].[spProviderService_Delete]
--Delete provider's service if it exists
	@ServiceId INT = 0,
    @pro_providerId NVARCHAR(450)
AS
IF EXISTS (SELECT * FROM [Delivery].[provider] 
WHERE [pro_serviceid]=@ServiceId AND [pro_userid] = @pro_providerId)
BEGIN
	DELETE FROM [Delivery].[provider] 
    WHERE [pro_serviceid]=@ServiceId AND [pro_userid] = @pro_providerId
END
