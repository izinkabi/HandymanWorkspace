﻿CREATE PROCEDURE [Delivery].[spOrderLookUpByService]
	--Getting all orders of the given service and its tasks
   @serviceId INT = 0
AS
BEGIN
	SELECT 
           o.[ord_id],o.[ord_datecreated] ,o.[ord_status],o.[ord_duedate] ,o.[ord_service_id],
            t.[task_id], t.[tas_title], t.[tas_date_started],  t.[tas_date_finished], t.[tas_duration],t.[tas_description],
            s.[order_id]
  FROM [Handyman_DB].[Request].[order] o
  inner join [Handyman_DB].[Request].[order_task] s ON s.[order_id] = o.[ord_id] 
  inner join [Handyman_DB].[Request].[task] t ON t.[task_id] = s.[task_id]
  WHERE o.[ord_service_id] = @serviceId 
  ORDER by o.[ord_datecreated] asc
  END