CREATE PROCEDURE [Service].[spCategoryInsert]
    @Title       VARCHAR,
    @Description VARCHAR          
AS
BEGIN
Set nocount on
	INSERT INTO [Service].[Category] (Title,Description)
	VALUES (@Title,@Description)	
END