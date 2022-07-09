CREATE PROCEDURE [Service].[spGetJobByCategoryId]--Get Jobs by category   
    @CategoryID     INT
AS
BEGIN
Set nocount on
	SELECT  JobPosition,CategoryID,ImageURL,JobDescription
	FROM [Service].[Job]
	WHERE CategoryID = @CategoryID
END
