CREATE PROCEDURE [dbo].[spRequestLookUp]
	@Id int = 0
	
AS
BEGIN
	SELECT * 
	FROM [dbo].[Request]
	WHERE Id=@Id
END
