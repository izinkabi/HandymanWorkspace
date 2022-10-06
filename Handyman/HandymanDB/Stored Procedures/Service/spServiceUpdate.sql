CREATE PROCEDURE [Service].[spServiceUpdate]
    @ServiceID          INT,
    @Name    VARCHAR,
    @CategoryID     INT,
    @ImageUrl      VARCHAR,
    @Description VARCHAR
AS
BEGIN
Set nocount on
	UPDATE [Service].[Service] 
	 SET Name=@Name, ImageUrl=@ImageUrl, Description=@Description
	WHERE Id = @ServiceID AND CategoryID = @CategoryID
END