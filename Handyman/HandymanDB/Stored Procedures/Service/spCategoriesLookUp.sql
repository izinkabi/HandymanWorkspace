CREATE PROCEDURE [Service].[spGetAllCategories]--Get All the Job Types/Categories
              
AS
BEGIN
Set nocount on
	SELECT *
	FROM [Service].[JobCategory]
END