CREATE PROCEDURE [Request].[spRequestInsert]
	
	@ConsumerID NVARCHAR(MAX),
	@ServiceId INT = 0,
    @DateCreated DATETIME,
    @Status INT,
    @DueDate DATETIME
AS
BEGIN
	INSERT INTO [Request].[request] 
    (
    [req_consumer_id],
    [req_datecreated],
    [req_duedate],
    [req_status],
    [req_service_id]
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

SELECT [req_id] FROM [Request].[request] WHERE [req_id] = @Id
END


