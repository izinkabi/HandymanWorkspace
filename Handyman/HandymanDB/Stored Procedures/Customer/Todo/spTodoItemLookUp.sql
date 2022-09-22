CREATE PROCEDURE [Customer].[spTodoItemLookUp]
	@Id int = 0
AS
BEGIN
	SELECT *
    FROM [Customer].[ToDo]
    WHERE Id = @Id
END
