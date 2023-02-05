CREATE PROCEDURE [Request].[spOrderUpdate]
	@ConsumerID NVARCHAR(450),
    @id int = 0,
    @status NCHAR(100),
    @dueDate DATETIME
AS
BEGIN
	UPDATE [Request].[order]
    SET   
    [ord_duedate] = @dueDate,
    [ord_status] = @status
    WHERE [ord_id] = @id AND [ord_consumer_id] = @ConsumerID
END
