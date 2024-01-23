CREATE PROCEDURE [Request].[spRequestUpdate]
	@ConsumerID NVARCHAR(450),
    @id int = 0,
    @status NCHAR(100),
    @dueDate DATETIME
AS
BEGIN
	UPDATE [Request].[order]
    SET   
    [order_duedate] = @dueDate,   
    [order_status] = @status
    WHERE [order_id] = @id AND [order_consumer_id] = @ConsumerID
END
