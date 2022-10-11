CREATE PROCEDURE [Service].[spServiceLookUp_GroupByCategory]
AS
BEGIN
Set nocount on
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (20) s.[serv_id]
      ,s.[serv_name]
      ,s.[serv_img]
      ,s.[serv_categoryid]
      ,s.[serv_datecreated]
      ,s.[serv_status]
      ,c.[cat_id]
      ,c.[cat_name]
      ,c.[cat_type]
      ,c.[cat_description]
  FROM [Handyman_DB].[Service].[service] s
  inner join [Handyman_DB].[Service].[category] c ON s.[serv_categoryid] = c.[cat_id]
  order by c.cat_id, s.serv_status desc
  end
