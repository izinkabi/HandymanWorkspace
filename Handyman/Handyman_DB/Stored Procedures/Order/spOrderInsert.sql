CREATE PROCEDURE [Delivery].[spOrderInsert]

    @order_datecreated datetime2 (7) ,
	@order_status nchar(10) ,
	@order_progress nchar(10) , 
    @order_employeeid NVARCHAR (450) , 
    @order_requestId INT 

 --   [order_id] [int] IDENTITY(1,1) NOT NULL,
	--[order_datecreated] [datetime2](7) NOT NULL,
	--[order_status] INT NOT NULL,
	--[order_progress] INT NULL, 
 --   [order_employeeid] NVARCHAR (450) NOT NULL, 
 --   [order_requestId] INT NOT NULL, 
AS
    IF NOT EXISTS (SELECT * FROM  [Delivery].[order] 
    WHERE [order_requestId] = @order_requestId AND [order_employeeid] = @order_employeeid)
BEGIN
	INSERT INTO [Delivery].[request] 
                (
                order_datecreated, 
                order_status, 
                order_progress, 
                order_employeeid, 
                order_requestId
                )

        VALUES (
                @order_datecreated, 
                @order_status, 
                @order_progress,
                @order_employeeid,
                @order_requestId
                )
END