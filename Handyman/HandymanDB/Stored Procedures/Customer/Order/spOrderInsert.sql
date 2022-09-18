CREATE PROCEDURE [Customer].[spOrderInsert]
	
    @ConsumerID NVARCHAR(450),
    @DateCreated DATETIME2(7) ,
    @Stage VARCHAR,
    @ServiceId       INT
    
    
AS
BEGIN
	INSERT INTO [Customer].[Order] (ConsumerID,DateCreated,Stage,ServiceId)
    VALUES (@ConsumerID,@DateCreated,@Stage,@ServiceId)
END
