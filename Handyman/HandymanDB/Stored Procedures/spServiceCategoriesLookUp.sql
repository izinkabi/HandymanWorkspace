CREATE PROCEDURE [dbo].[spServiceCategoriesLookUp]
	@Id int 
	
AS
BEGIN
	SELECT *
	FROM [dbo].[ServiceCategory]
	WHERE Id = @Id
END
