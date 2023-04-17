CREATE PROCEDURE [Service].[spCustomServiceInsert]
--inserting a custom service 
    @Id int,
    @title NVARCHAR(50) , 
    @description NVARCHAR(100) , 
    @imageUrl NVARCHAR(2000) , 
    @originalServiceId INT
    
AS
  IF EXISTS(SELECT * FROM [Service].service WHERE serv_id = @originalServiceId)

BEGIN
  
	INSERT  INTO [Service].[customService] (title,[description],imageUrl,originalServiceId)
    VALUES (@title,@description,@imageUrl,@originalServiceId)
    
END
