CREATE PROCEDURE [Request].[spOrderUpdate]
	@ConsumerID NVARCHAR(MAX),
    @id int = 0,
    @dateCreated DATETIME,
    @status NCHAR(100),
    @dueDate DATETIME
AS
BEGIN
	UPDATE [Request].[order]
    SET  

    [ord_datecreated] = @dateCreated,
    [ord_duedate] = @dateCreated,
    [ord_status] = @status
    WHERE [ord_id] = @id AND [ord_consumer_id] = @ConsumerID
END
