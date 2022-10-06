CREATE PROCEDURE [Customer].[spTodoItemsLookUpByOrderId]
	@OrderId int
AS
BEGIN
	SELECT *
    FROM [Customer].[ToDo]
    WHERE OrderId = @OrderId
END
