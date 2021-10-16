CREATE PROCEDURE [dbo].[spProvidersServiceInsert]
	@Id int,
	@ServiceProviderId int,
	@ServiceId int
AS
Begin
	INSERT INTO ProvidersService(ServiceId,ServiceProviderId)
	VALUES(@ServiceId,@ServiceProviderId)
End
