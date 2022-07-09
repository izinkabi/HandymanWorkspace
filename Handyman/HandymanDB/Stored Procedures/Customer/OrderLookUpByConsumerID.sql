CREATE PROCEDURE [Customer].[OrderLookUpByConsumerID]
	@ConsumerID VARCHAR 

AS
BEGIN
	SELECT DateCreated,Stage,JobId      
	FROM [Customer].[Order]
	WHERE ConsumerID = @ConsumerID
END