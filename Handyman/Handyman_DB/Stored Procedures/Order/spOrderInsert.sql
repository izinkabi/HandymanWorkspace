CREATE PROCEDURE [Request].[spOrderInsert]
	@ServiceId INT = 0,
    @DateCreated DATETIME,
    @Status NCHAR(100),
    @DueDate DATETIME
AS
BEGIN
	INSERT INTO [Request].[order] ([ord_datecreated],[ord_duedate],[ord_status],[ord_service_id])
    VALUES(@DateCreated,@DueDate,@Status,@ServiceId)
END
