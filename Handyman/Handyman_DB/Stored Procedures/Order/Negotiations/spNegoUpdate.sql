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
    SET [IsCompleted] = @IsCompleted,
        [Order_DueDate] = @Order_DueDate, 
        [LastDateModified] = @LastDateModified

    WHERE [Id] = @Id
END
