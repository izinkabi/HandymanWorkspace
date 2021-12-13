CREATE PROCEDURE [dbo].[spProvidersServiceDelete]
	@ServiceId int = 0
	
AS
BEGIN
	DELETE FROM [dbo].[ProvidersService] 
	WHERE ServiceId=@ServiceId
END	