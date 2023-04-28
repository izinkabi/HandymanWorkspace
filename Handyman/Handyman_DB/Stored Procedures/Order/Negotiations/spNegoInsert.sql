CREATE PROCEDURE [Request].[spNegoInsert]
--Insert a new negotiation
	@OrderId INT = 0,
	@DateCreated DATETIME,
    @PriceId INT = 0,
    @IsCompleted INT = 0,
    @Order_DueDate DATETIME,
    @LastDateModified DATETIME
AS
BEGIN

	INSERT INTO [Request].[negotiation] 
    (
    OrderId,
	DateCreated,
    PriceId ,
    IsCompleted ,
    Order_DueDate,
    LastDateModified
    )

    VALUES(
    @OrderId ,
	@DateCreated,
    @PriceId ,
    @IsCompleted ,
    @Order_DueDate,
    @LastDateModified
    )
END
