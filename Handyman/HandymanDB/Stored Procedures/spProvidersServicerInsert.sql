CREATE PROCEDURE [dbo].[spProvidersServicerInsert]
	@ServiceProviderId int,
	@ServiceId int
AS
Begin
	INSERT INTO ProvidersService(ServiceId,ServiceProviderId)
	VALUES(@ServiceId,@ServiceProviderId)
End
