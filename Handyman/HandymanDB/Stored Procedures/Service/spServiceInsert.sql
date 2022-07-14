CREATE PROCEDURE [Service].[spServiceInsert]--Creating Employment
    @Name    VARCHAR,
    @CategoryID     INT,
    @ImageUrl   VARCHAR,
    @Description VARCHAR 
AS
BEGIN
Set nocount on
	INSERT INTO [Service].[Service] (Name,CategoryID,ImageURL,Description)
	VALUES (@Name,@CategoryID,@ImageUrl,@Description)	
END
