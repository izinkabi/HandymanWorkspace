CREATE PROCEDURE [ServiceDelivery].[spOrderLookUpById]
	@Id int = 0
AS
BEGIN
	SELECT *
    FROM [Customer].[Order]
    WHERE Id = @Id

END
