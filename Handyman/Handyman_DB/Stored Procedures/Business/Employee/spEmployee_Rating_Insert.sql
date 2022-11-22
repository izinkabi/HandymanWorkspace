CREATE PROCEDURE [Delivery].[spEmployee_Rating_Insert]

	@providerId NVARCHAR(450),
    @ratingId INT = 0

AS 
BEGIN 
      
	INSERT INTO [Delivery].[employee_ratings] ([employee_id],[rating_id])
    VALUES (@providerId,@ratingid)

END