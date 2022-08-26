CREATE PROCEDURE [Customer].[OrderLookUpByConsumerID]
	@ConsumerID NVARCHAR(450) 

AS
BEGIN
	SELECT *    
	FROM [Customer].[Order]
	WHERE ConsumerID = @ConsumerID
END