CREATE PROCEDURE [Service].[spUpdateService]
	@serv_id int = 0
    ,@serv_name VARCHAR(100)
    ,@serv_img VARCHAR(MAX)   
    ,@serv_datecreated DATETIME
    ,@serv_status nchar(100)
    
AS
BEGIN
	UPDATE [Handyman_DB].[Service].[service]
    SET 
       [serv_name] = @serv_name
      ,[serv_img] = @serv_img
      ,[serv_datecreated] = @serv_datecreated
      ,[serv_status] = @serv_status
     WHERE 
     [serv_id] = @serv_id
END
