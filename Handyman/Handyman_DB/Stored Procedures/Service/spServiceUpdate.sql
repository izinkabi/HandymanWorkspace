CREATE PROCEDURE [Service].[spServiceUpdate]
--Update the service
  @id int = 0,
  @name nvarchar(50),
  @img nvarchar(50),
  @datecreated datetime,
  @priceId int,
  @categoryId int
AS
IF EXISTS (SELECT * FROM [Service].[service] WHERE [serv_id]=@id)
BEGIN
	UPDATE [Service].[service]
    SET [serv_name] = @name, [serv_img]=@img, [serv_categoryid] = @categoryId , [price_id] = @priceId
    WHERE [serv_id] = @id
END
