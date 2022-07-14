CREATE PROCEDURE [Customer].[spOrderInsert]
	
    @ConsumerID NVARCHAR  ,
    @DateCreated DATETIME2  ,
    @Stage       VARCHAR    ,
    @JobId       INT          
AS
BEGIN
	INSERT INTO [Customer].[Order] (ConsumerID,DateCreated,Stage,JobId)
    VALUES (@ConsumerID,@DateCreated,@Stage,@JobId)
END
