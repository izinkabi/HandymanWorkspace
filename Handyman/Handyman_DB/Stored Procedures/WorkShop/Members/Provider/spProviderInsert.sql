CREATE PROCEDURE [Delivery].[spProviderInsert]
	   @ServiceId INT = 0,
       @pro_providerId NVARCHAR(450)
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM [Delivery].[provider] WHERE [pro_serviceid] = @ServiceId AND [pro_userid]=@pro_providerId)
	INSERT INTO [Delivery].[provider] ([pro_serviceid],[pro_userid])
    VALUES (@ServiceId,@pro_providerId)
END
