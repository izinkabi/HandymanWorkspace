CREATE PROCEDURE [Service].[spServiceLookUpByCategoryId]--Get services by category   
    @Id     INT
AS
BEGIN
Set nocount on
	SELECT s.Name, s.Description, c.CategoryName + ', ' + c.Type + ' ' + c.CategoryDescription AS Service
FROM [Service].[Service] AS s, [Serveice].[Category] AS c
WHERE c.Id = @Id AND s.CategoryId=c.Id;
END
