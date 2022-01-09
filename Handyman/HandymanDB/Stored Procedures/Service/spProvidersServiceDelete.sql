CREATE PROCEDURE [dbo].[spProvidersServiceDelete]
	@Id int = 0
	
AS
BEGIN
	DELETE FROM [dbo].[ProvidersService] 
	WHERE Id=@Id
END	