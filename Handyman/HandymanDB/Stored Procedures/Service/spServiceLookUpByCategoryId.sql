CREATE PROCEDURE [Service].[spServiceLookUpByCategoryId]--Get services by category   
    @Id     INT
AS
BEGIN
Set nocount on
SELECT s.Name, s.Description, c.CategoryName + ', ' + c.Type + ' ' + c.CategoryDescription AS Service
FROM [Service].[Service] AS s, [Service].[Category] AS c
WHERE c.CategoryId = @Id AND s.CategoryId=c.CategoryId;
END
