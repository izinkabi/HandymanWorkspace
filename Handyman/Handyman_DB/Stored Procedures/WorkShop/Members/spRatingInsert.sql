CREATE PROCEDURE [Delivery].[spRatingInsert]
	 @Id INT,
     @stars INT,
     @review NVARCHAR(MAX),
     @recommendation NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [Delivery].[ratings] ([rate_stars],[rate_review],[rate_recommendation])
    VALUES(@stars,@review,@recommendation)

    DECLARE @rateId INT = SCOPE_IDENTITY();
    SELECT [rate_id] 
    FROM   [Delivery].[ratings] 
    WHERE [rate_id] = @rateId
END
