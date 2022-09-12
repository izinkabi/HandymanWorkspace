﻿CREATE PROCEDURE [Service].[spTodoInsert]
	@ItemName VARCHAR(150), 
    @Description VARCHAR(250), 
    @OrderId INT, 
    @Type INT, 
    @Status INT 
AS
BEGIN
	INSERT INTO [Service].[Todo] (ItemName, Description, OrderId, Type, Status)
    VALUES(@ItemName, @Description, @OrderId, @Type, @Status)
END
