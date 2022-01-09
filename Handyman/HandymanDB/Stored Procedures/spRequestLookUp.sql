CREATE PROCEDURE [dbo].[spRequestLookUp] 
--Looking for a request using a consumer's id
	@Id int = 0
	
AS
BEGIN
	SELECT * 
	FROM [dbo].[Request]
	WHERE ConsumerId=@Id
END
