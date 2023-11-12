CREATE PROCEDURE [Request].[spTaskLookUpByRequest]
--Lookup task by the request
	@orderId int = 0
AS
BEGIN

	SELECT t.[task_id], t.[tas_title],t.[tas_date_started],
           t.[tas_date_finished] ,t.[tas_duration], 
           t.[tas_status] ,t.[tas_description],t.[tas_service_id]

    FROM [Request].[task] t
    INNER JOIN [Request].[request_task] r ON r.[task_id] = t.[task_id]
    WHERE r.[request_id] = @orderId

END
