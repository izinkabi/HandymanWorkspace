CREATE PROCEDURE [Service].[spCustomServicesLookUp]
	@serviceId int 
AS

IF EXISTS (SELECT * FROM [Service].[customService] WHERE [originalServiceId] = @serviceId)
BEGIN
	SELECT * 
    FROM [Service].[customService]
    WHERE [customService].[originalServiceId] = @serviceId
END
