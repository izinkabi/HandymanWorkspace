CREATE PROCEDURE [Customer].[spOrderInsert]
	
    @ConsumerID NVARCHAR  NOT NULL,
    @DateCreated DATETIME2  NOT NULL,
    @Stage       VARCHAR    NOT NULL,
    @JobId       INT            NOT NULL
AS
BEGIN
	INSERT INTO [Customer].[Order] (ConsumerID,DateCreated,Stage,JobId)
    VALUES (@ConsumerID,@DateCreated,@Stage,@JobId)
END
