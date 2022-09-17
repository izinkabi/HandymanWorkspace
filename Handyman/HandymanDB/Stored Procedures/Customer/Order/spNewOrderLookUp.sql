CREATE PROCEDURE [Customer].[spNewOrderLookUp]
	    @ConsumerID NVARCHAR(450),
        @DateCreated DATETIME2(7),
        @ServiceId INT
AS
BEGIN
	SELECT Id    
	FROM [Customer].[Order]
	WHERE ConsumerID = @ConsumerID AND DateCreated=@DateCreated AND ServiceId = @ServiceId
END