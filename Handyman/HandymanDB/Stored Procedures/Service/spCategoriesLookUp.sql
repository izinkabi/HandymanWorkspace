CREATE PROCEDURE [Service].[spCategoriesLookUp]--Get All the Job Types/Categories
              
AS
BEGIN
Set nocount on
	SELECT *
	FROM [Service].[Category]
END