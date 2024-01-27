CREATE PROCEDURE [Service].[spService_Insert]
--Insert a new Service
	@serv_name nchar(30),
	@serv_img varchar(max),
	@serv_categoryid int = 0,
	@serv_datecreated datetime, 
    @price_id INT  
AS
IF EXISTS (SELECT * FROM [Service].[category] WHERE [cat_id] = @serv_categoryid )
BEGIN
    IF NOT EXISTS (SELECT * FROM [Service].[service] WHERE [serv_name] = @serv_name )

    INSERT INTO [Service].[service] (serv_name,serv_datecreated,serv_img,serv_categoryid,price_id)
    VALUES (@serv_name,@serv_datecreated,@serv_img,@serv_categoryid,@price_id)
END
