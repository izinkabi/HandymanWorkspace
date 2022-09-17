CREATE PROCEDURE [Customer].[spOrderLookUpByConsumerID]
	--Getting the consumer's order(s)
    @ConsumerID NVARCHAR(450) 

AS
BEGIN
	SELECT *    
	FROM [Customer].[Order]
	WHERE ConsumerID = @ConsumerID
END