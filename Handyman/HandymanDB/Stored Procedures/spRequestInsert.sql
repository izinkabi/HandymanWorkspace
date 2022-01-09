CREATE PROCEDURE [dbo].[spRequestInsert]
	@ConsumerId int = 0,
	@ProvidersServiceId int = 0,
	@RequestStatus varchar(50)='',
	@StartTime datetime,
	@FinistTime datetime

AS
BEGIN
	INSERT INTO Request(ConsumerId,ProvidersServiceId,Status,StartTime,FinishTime)
	VALUES(@ConsumerId,@ProvidersServiceId,@RequestStatus,@StartTime,@FinistTime)
END
