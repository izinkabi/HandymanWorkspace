CREATE PROCEDURE [Request].[spNegoUpdate]
--Update the negotiation
    @Id INT = 0,
	@IsCompleted INT = 0,
    @Order_DueDate DATETIME,
    @LastDateModified DATETIME
AS
--Validate the Id
IF EXISTS (SELECT * FROM [Request].negotiation 
           WHERE [Id] = @Id)
BEGIN
	UPDATE [Request].[negotiation] 
    SET [neg_IsCompleted] = @IsCompleted,
        [neg_Order_DueDate] = @Order_DueDate, 
        [neg_LastDateModified] = @LastDateModified

    WHERE [Id] = @Id
END
