CREATE PROCEDURE [Request].[spServiceLookUp_ByOrder]
--A service of an order
	@orderId int = 0
AS
IF EXISTS (SELECT * FROM [Request].[order] WHERE [ord_id] = @orderId)
BEGIN
	SELECT s.serv_categoryid, s.serv_datecreated, s.serv_img, s.serv_name, s.serv_status,
            c.cat_name,cat_description,cat_type

    FROM [Service].[service] s
    INNER JOIN [Service].[category] c ON c.cat_id = s.serv_categoryid
    INNER JOIN [Request].[order] o ON o.ord_id = @orderId
  
    WHERE s.serv_id = o.ord_service_id
END
