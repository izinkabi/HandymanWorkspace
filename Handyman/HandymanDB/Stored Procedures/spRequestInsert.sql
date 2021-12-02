CREATE PROCEDURE [dbo].[spRequestInsert]
	@ConsumerId int = 0,
	@ProvidersServiceId int
AS
BEGIN
	INSERT INTO Request(ConsumerId,ProvidersServiceId)
	VALUES(@ConsumerId,@ProvidersServiceId)
END
