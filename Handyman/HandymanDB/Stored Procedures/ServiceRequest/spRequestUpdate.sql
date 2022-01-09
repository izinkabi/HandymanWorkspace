CREATE PROCEDURE [dbo].[spRequestUpdate]
--Updating the customer's request
	@Id int,
	@RequestStatus varchar(50)='',
	@FinishTime datetime
AS
BEGIN
	Set nocount on
	UPDATE [dbo].[Request]
	SET RequestStatus=@RequestStatus,FinishTime=@FinishTime
	WHERE Id=@Id
End
