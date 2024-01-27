CREATE PROCEDURE [Delivery].[spEmployee_Rating_Insert]

	@memberId NVARCHAR(450),
    @ratingId INT = 0

AS 
BEGIN 
      
	INSERT INTO [Delivery].[employee_ratings] ([employee_id],[rating_id])
    VALUES (@memberId,@ratingid)

END