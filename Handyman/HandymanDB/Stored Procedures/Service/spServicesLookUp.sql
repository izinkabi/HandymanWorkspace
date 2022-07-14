CREATE PROCEDURE [Service].[spServicesLookUp]--Get all Jobs
              
AS
BEGIN
Set nocount on
	SELECT *
	FROM [Service].[Service]	
END