CREATE PROCEDURE [dbo].[spJwtToken_Delete]
	@userId NVARCHAR(450)
AS
BEGIN
	DELETE FROM [dbo].[AspNetUserTokens]
    WHERE [UserId]=@userId
END
