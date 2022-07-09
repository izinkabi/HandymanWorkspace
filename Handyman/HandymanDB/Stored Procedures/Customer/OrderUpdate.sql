CREATE PROCEDURE [Customer].[OrderUpdate]

	@Id	INT = 0 ,
	@ConsumerId INT,
	@DateCreated DATETIME2  NOT NULL,
    @Stage       VARCHAR    NOT NULL,
    @JobId       INT            NOT NULL
AS
BEGIN
--Updating the Customer's Order with its ID
	UPDATE [Customer].[Order]
	SET	DateCreated = @DateCreated, Stage = @Stage, JobId = @JobId
	WHERE Id = @Id AND ConsumerID = @ConsumerId 
END
