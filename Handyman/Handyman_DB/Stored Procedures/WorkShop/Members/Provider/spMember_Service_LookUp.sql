CREATE PROCEDURE [Delivery].[spMember_Service_LookUp]
	@member_profileId NVARCHAR(450)
	
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
    FROM [Delivery].[member] m
    INNER JOIN [Service].[service] s ON s.[serv_id] = m.[member_serviceid]
    INNER JOIN [Service].[category] c ON c.[cat_id] = s.[serv_categoryid]
    WHERE m.[member_profileId] = @member_profileId

END
