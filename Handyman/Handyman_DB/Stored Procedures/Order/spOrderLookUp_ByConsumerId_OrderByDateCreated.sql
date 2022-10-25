CREATE PROCEDURE [Request].[spOrderLookUp_ByConsumerId_OrderByDateCreated]
	@consumerID int = 0
	
AS
BEGIN
	SELECT TOP (20) 
      o.[ord_datecreated]
      ,o.[ord_status]
      ,o.[ord_duedate]
      ,o.[ord_service_id],
        t.[task_id], 
        t.[tas_title], 
        t.[tas_date_started], 
        t.[tas_date_finished], 
        t.[tas_duration]
  FROM [Handyman_DB].[Request].[order] o
  inner join [Handyman_DB].[Request].[task] t ON o.[ord_id] = t.[task_id]
  inner join [Handyman_DB].[Request].[order_task] s ON s.[order_id] = o.[ord_id] AND s.[task_id] = t.[task_id]
  order by o.ord_datecreated asc
 END
