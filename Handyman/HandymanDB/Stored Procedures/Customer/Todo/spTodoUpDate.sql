CREATE PROCEDURE [Customer].[spTodoUpDate]
	@StartDate DATETIME2,
    @OrderId int
    

AS
BEGIN
Set nocount on
	UPDATE [Customer].[ToDo] 
	SET StartDate=@StartDate , Status = 1
	WHERE OrderId = @OrderId
END