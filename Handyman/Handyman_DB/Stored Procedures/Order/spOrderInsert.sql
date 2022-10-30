CREATE PROCEDURE [Request].[spOrderInsert]
    @ConsumerID NVARCHAR(MAX),
	@ServiceId INT = 0,
    @DateCreated DATETIME,
    @Status INT,
    @DueDate DATETIME
AS
BEGIN
	INSERT INTO [Request].[order] 
    (
    [ord_consumer_id],
    [ord_datecreated],
    [ord_duedate],
    [ord_status],
    [ord_service_id]
    )
    
    VALUES
    (
    @ConsumerID,
    @DateCreated,
    @DueDate,
    @Status,
    @ServiceId
    )
    

DECLARE @Id INT = SCOPE_IDENTITY();

SELECT [ord_id] FROM [Request].[order] WHERE [ord_id] = @Id
END
