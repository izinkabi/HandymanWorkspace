CREATE PROCEDURE [Request].[spTaskDelete]
	@taskId int = 0
AS
IF EXISTS (SELECT * FROM [Request].[order_task] WHERE [task_id]=@taskId)
BEGIN
--Delete the link
    DELETE 
    FROM [Request].[order_task]
    WHERE [task_Id] = @taskId

--Delete the task
	DELETE 
    FROM [Request].[task]
    WHERE [task_id] = @taskId

   
END
