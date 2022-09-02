CREATE PROCEDURE [Customer].[spOrderLookUpByConsumerID]
	@ConsumerID NVARCHAR(450) 

AS
BEGIN
	SELECT *    
	FROM [Customer].[Order]
	WHERE ConsumerID = @ConsumerID
END