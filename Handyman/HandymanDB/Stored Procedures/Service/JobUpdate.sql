CREATE PROCEDURE [Service].[spJobUpdate]
    @JobID          INT,
    @JobPosition    VARCHAR,
    @CategoryID     INT,
    @JobImage       VARCHAR,
    @JobDescription VARCHAR
AS
BEGIN
Set nocount on
	UPDATE [Service].[Job] 
	 SET JobPosition=@JobPosition, ImageURL=@JobImage, JobDescription=@JobDescription
	WHERE JobID = @JobID AND CategoryID = @CategoryID
END