CREATE PROCEDURE [Customer].[spOrderDelete]
--Delete the order after the request due to primary key constraints
	@Id int = 0
	
AS
BEGIN
	DELETE FROM [Customer].[Order]
    WHERE Id = @Id
END
