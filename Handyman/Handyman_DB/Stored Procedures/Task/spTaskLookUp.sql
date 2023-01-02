CREATE PROCEDURE [Request].[spTaskLookUp]
	@taskId int = 0
AS
BEGIN
	SELECT * FROM 
    [Request].[task]
    WHERE [task_id] = @taskId
END
