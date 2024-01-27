CREATE PROCEDURE [Request].[spTaskPriceLookUp]
--Looking up prices for a task
--#There is a risk of having rows with same price id and same task#
--*Looking for a way of passing a TaskPrice Id*
	@TaskId INT = 0
AS
BEGIN
	SELECT p.* 
    FROM [Request].[TaskPrice] as tp,
         [Service].[price] as p
    WHERE tp.TaskId = @TaskId AND p.Id = tp.PriceId
END
