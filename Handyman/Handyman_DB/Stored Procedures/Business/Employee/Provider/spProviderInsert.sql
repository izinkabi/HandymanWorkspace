CREATE PROCEDURE [Delivery].[spProviderInsert]
	   @ServiceId INT = 0,
       @pro_providerId NVARCHAR(450)
AS
BEGIN
	INSERT INTO [Delivery].[provider] ([pro_serviceid],[pro_userid])
    VALUES (@ServiceId,@pro_providerId)
END
