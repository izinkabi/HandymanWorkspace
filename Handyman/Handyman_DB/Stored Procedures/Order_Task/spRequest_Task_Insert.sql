CREATE PROCEDURE [Request].[spRequest_Task_Insert]
	@TaskId int = 0,
	@requestId int
AS
BEGIN
SET NOCOUNT ON
	INSERT INTO [Request].[request_task] ([task_id],[request_id])
    VALUES (@TaskId,@requestId)
END
