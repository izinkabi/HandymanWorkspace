CREATE PROCEDURE [Service].[spJobInsert]--Creating Employment
    @JobPosition    VARCHAR,
    @CategoryID     INT,
    @JobImage       VARCHAR,
    @JobDescription VARCHAR 
AS
BEGIN
Set nocount on
	INSERT INTO [Service].[Job] (JobPosition,CategoryID,ImageURL,JobDescription)
	VALUES (@JobPosition,@CategoryID,@JobImage,@JobDescription)	
END
