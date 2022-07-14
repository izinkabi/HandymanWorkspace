CREATE PROCEDURE [Service].[spGetAllServices]
	
AS
BEGIN
Set nocount on
	SELECT  [Id],[Title],[Desciption],[CategoryId]
	FROM [Service].[Services]
END