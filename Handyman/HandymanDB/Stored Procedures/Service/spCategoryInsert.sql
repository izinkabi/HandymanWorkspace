CREATE PROCEDURE [Service].[spCategoryInsert]
    @Title       VARCHAR,
    @Type VARCHAR          
AS
BEGIN
Set nocount on
	INSERT INTO [Service].[Category] (Category_Title,Category_Type)
	VALUES (@Title,@Type)	
END