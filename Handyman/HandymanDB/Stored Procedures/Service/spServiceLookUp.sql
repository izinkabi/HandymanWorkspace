CREATE PROCEDURE [Service].[spServiceLookUp]--Get a single service using an Id
	@Id int 
AS
BEGIN
	SELECT s.Name, s.Description, c.CategoryName + ', ' + c.Type + ' ' + c.CategoryDescription AS Service
FROM [Service].[Service] AS s, [Service].[Category] AS c
WHERE s.Id=@Id AND s.CategoryId=c.Id;

END
