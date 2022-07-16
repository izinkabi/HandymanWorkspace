CREATE PROCEDURE [Customer].[OrderUpdate]

	@Id	INT = 0 ,
	@ConsumerId INT,
	@DateCreated DATETIME2 ,
    @Stage       VARCHAR ,
    @ServiceId       INT
AS
BEGIN
--Updating the Customer's Order with its ID
	UPDATE [Customer].[Order]
	SET	DateCreated = @DateCreated, Stage = @Stage, ServiceId = @ServiceId
	WHERE Id = @Id AND ConsumerID = @ConsumerId 
END
