CREATE PROCEDURE [Request].[spOrder_Task_Insert]
	@TaskId int = 0,
	@OrderId int
AS
BEGIN
SET NOCOUNT ON
	INSERT INTO [Request].[order_task] ([task_id],order_id)
    VALUES (@TaskId,@OrderId)
END
