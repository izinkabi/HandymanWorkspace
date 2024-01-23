CREATE PROCEDURE [Request].[spNegoInsert]
--Insert a new negotiation
	@RequestId INT = 0,
	@DateCreated DATETIME,
    @PriceId INT = 0,
    @IsCompleted INT = 0,
    @Order_DueDate DATETIME,
    @LastDateModified DATETIME
AS
BEGIN

	INSERT INTO [Request].[negotiation] 
    (
    neg_requestId,
	neg_DateCreated,
    neg_PriceId ,
    neg_IsCompleted ,
    neg_Order_DueDate,
    neg_LastDateModified
    )

    VALUES(
    @RequestId,
	@DateCreated,
    @PriceId ,
    @IsCompleted ,
    @Order_DueDate,
    @LastDateModified
    )
END
