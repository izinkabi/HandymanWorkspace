CREATE PROCEDURE [dbo].[spProfileLookUp]
	@userId VARCHAR(450)
AS
BEGIN
	SELECT *
    FROM [dbo].[profile]
    WHERE [UserId]=@userId
END
