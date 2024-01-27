CREATE PROCEDURE [Request].[spServiceLookUpBy_Id]
	@serviceId int = 0
AS
BEGIN
	SELECT s.[serv_id]
      ,s.[serv_name]
      ,s.[serv_img]
      ,s.[serv_categoryid]
      ,s.[serv_datecreated]
      ,s.[serv_status]
      ,s.[price_id]
      ,c.[cat_id]
      ,c.[cat_name]
      ,c.[cat_type]
      ,c.[cat_description]
  FROM [Handyman_DB].[Service].[service] s

  inner join [Handyman_DB].[Service].[category] c ON s.[serv_categoryid] = c.[cat_id]
   WHERE [serv_id] = @serviceId
  order by c.cat_id, s.serv_status desc
 
END