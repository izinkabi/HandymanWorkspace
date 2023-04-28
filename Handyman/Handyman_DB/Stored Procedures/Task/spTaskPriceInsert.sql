CREATE PROCEDURE [Request].[spTaskPriceInsert]
--Insert a new TaskPrice
	@TaskId int = 0,
	@PriceId int
AS
BEGIN
	INSERT INTO [Request].[TaskPrice] ([TaskId],[PriceId])
    VALUES (@TaskId,@PriceId)
END
