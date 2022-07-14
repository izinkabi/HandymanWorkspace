CREATE PROCEDURE [Service].[spCategoryInsert]
    @CategoryName       VARCHAR,
    @Description VARCHAR,
    @Type   VARCHAR
AS
BEGIN
Set nocount on
	INSERT INTO [Service].[Category] (CategoryName,CategoryDescription,Type)
	VALUES (@CategoryName,@Description,@Type)	
END