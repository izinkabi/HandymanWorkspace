CREATE PROCEDURE [dbo].[spServiceInsert]
	
	@name VARCHAR (45),
	@serviceCategoryId INT,
	@description VARCHAR (100)
AS
Begin
	INSERT INTO Service ([Name],ServiceCategoryId,[Description])
	VALUES (@Name,@serviceCategoryId,@description)
End
