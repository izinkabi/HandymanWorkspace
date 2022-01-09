CREATE PROCEDURE [dbo].[spRequestInsert]
	@ConsumerId int = 0,
	@ProvidersServiceId int = 0,
	@RequestStatus varchar(50)='',
	@StartTime datetime2(7)
	

AS
BEGIN
	INSERT INTO Request(ConsumerId,ProvidersServiceId,RequestStatus,StartTime)
	VALUES(@ConsumerId,@ProvidersServiceId,@RequestStatus,@StartTime)
END
