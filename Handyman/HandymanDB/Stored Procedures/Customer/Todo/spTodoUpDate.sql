CREATE PROCEDURE [Customer].[spTodoUpDate]
	@StartDate DATETIME2,
	@EndDate DATETIME2,
    @OrderId int

AS
BEGIN
Set nocount on
	UPDATE [Customer].[ToDo] 
	SET StartDate=@StartDate, @EndDate=@EndDate
	WHERE OrderId = @OrderId
END