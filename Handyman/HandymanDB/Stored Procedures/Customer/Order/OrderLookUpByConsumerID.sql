CREATE PROCEDURE [Customer].[OrderLookUpByConsumerID]
	@ConsumerID VARCHAR 

AS
BEGIN
	SELECT DateCreated,Stage,ServiceId      
	FROM [Customer].[Order]
	WHERE ConsumerID = @ConsumerID
END