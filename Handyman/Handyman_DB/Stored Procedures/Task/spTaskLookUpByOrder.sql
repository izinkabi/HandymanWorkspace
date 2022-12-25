CREATE PROCEDURE [Delivery].[spTaskLookUpByOrder]
	@orderId int = 0
AS
BEGIN

	SELECT t.[task_id], t.[tas_title],t.[tas_date_started],
           t.[tas_date_finished] ,t.[tas_duration], 
           t.[tas_status] ,t.[tas_description],t.[tas_service_id]

    FROM [Request].[task] t
    INNER JOIN [Request].[order_task] o ON o.[task_id] = t.[task_id]
    WHERE o.[order_id] = @orderId

END
