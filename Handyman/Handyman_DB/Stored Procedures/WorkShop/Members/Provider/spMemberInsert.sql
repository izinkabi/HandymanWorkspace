CREATE PROCEDURE [Delivery].[spMemberInsert]
	   @ServiceId INT = 0,
       @member_profileId NVARCHAR(450)
AS
BEGIN
    IF NOT EXISTS(SELECT * FROM [Delivery].[member] WHERE [member_serviceid] = @ServiceId AND [member_profileId]=@member_profileId)
	INSERT INTO [Delivery].[member] ([member_serviceid],[member_profileId])
    VALUES (@ServiceId,@member_profileId)
END
