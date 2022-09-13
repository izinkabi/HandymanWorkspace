CREATE PROCEDURE [Service].[spTodoItemsLookUpByOrderId]
	@OrderId int
AS
BEGIN
	SELECT *
    FROM [Service].[ToDo]
    WHERE Id = @OrderId
END
