CREATE PROCEDURE [Customer].[spOrderInsert]
	
    @ConsumerID NVARCHAR ,
    @DateCreated DATETIME2 ,
    @Stage       VARCHAR   ,
    @ServiceId       INT    
AS
BEGIN
	INSERT INTO [Customer].[Order] (ConsumerID,DateCreated,Stage,ServiceId)
    VALUES (@ConsumerID,@DateCreated,@Stage,@ServiceId)
END
