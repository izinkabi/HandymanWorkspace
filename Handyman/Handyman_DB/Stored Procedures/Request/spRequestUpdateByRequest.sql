CREATE PROCEDURE [Delivery].[spRequestUpdateByRequest]
	@requestId int = 0,
	@status NCHAR(100)
AS
BEGIN
	UPDATE [Request].[request]
    SET
    [req_status] = @status
    WHERE [req_id] = @requestId

END
