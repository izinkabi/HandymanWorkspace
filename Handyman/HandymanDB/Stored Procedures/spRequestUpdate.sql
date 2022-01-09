CREATE PROCEDURE [dbo].[spRequestUpdate]
--Updating the customer's request
	@Id int,
	@ProvidersServiceId int = 0,
	@RequestStatus varchar(50)='',
	@StartTime datetime,
	@FinishTime datetime
AS
BEGIN
	Set nocount on
	UPDATE [dbo].[Request]
	SET ProvidersServiceId=@ProvidersServiceId,Status=@RequestStatus,StartTime=@StartTime,FinishTime=@FinishTime
	WHERE Id=@Id
End
