CREATE PROCEDURE [Delivery].[spRequestLookUpByProvider]
	@providerId NVARCHAR(450)
AS
BEGIN
	SELECT *
    FROM [Delivery].[request]
    WHERE [req_employeeId] = @providerId
END
