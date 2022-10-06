CREATE PROCEDURE [Customer].[spTodoDelete]
    @OrderId int
	
AS
BEGIN
	DELETE FROM [Customer].[ToDo]
    WHERE OrderId = @OrderId
END