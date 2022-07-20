CREATE PROCEDURE [Service].[spServicesLookUp]--Get all Jobs
              
AS
BEGIN
Set nocount on
	SELECT s.Id,s.Name, s.Description, c.CategoryName ,  c.Type , c.CategoryDescription, s.ImageUrl,c.CategoryId 
FROM [Service].[Service] AS s, [Service].[Category] AS c
WHERE s.CategoryId=c.CategoryId;	
END