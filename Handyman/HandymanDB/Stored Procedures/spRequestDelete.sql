CREATE PROCEDURE [dbo].[spRequestDelete]
	@Id int = 0
AS
BEGIN
	DELETE FROM [dbo].[Request] 
	WHERE Id=@Id
END	
