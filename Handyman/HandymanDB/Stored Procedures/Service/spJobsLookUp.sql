CREATE PROCEDURE [Service].[spGetAllJobs]--Get all Jobs
              
AS
BEGIN
Set nocount on
	SELECT *
	FROM [Service].[Job]	
	
END