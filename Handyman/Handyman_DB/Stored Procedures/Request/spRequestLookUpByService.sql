CREATE PROCEDURE [Delivery].[spRequestLookUpByService]
	--Getting all requests of the given service and its tasks
   @serviceId INT = 0
AS
BEGIN
	SELECT 
            r.[req_id],r.[req_datecreated] ,r.[req_status],r.[req_duedate] ,r.[req_service_id],
            t.[task_id], t.[tas_title], t.[tas_date_started],  t.[tas_date_finished], t.[tas_duration],t.[tas_description],
            s.[request_id]
  FROM [Handyman_DB].[Request].[request] r
  inner join [Handyman_DB].[Request].[request_task] s ON s.[request_id] = r.[req_id] 
  inner join [Handyman_DB].[Request].[task] t ON t.[task_id] = s.[task_id]
  WHERE r.[req_service_id] = @serviceId 
  order by r.[req_datecreated] asc
  END
