CREATE PROCEDURE [Delivery].[spCancelledRequestLookUp]
	@employeeId NVARCHAR(450)
AS
BEGIN
	SELECT *
    FROM [Delivery].[request] 
    WHERE  [request].[req_status] = 11 AND [req_employeeid] = @employeeId
    ORDER BY [req_datecreated] DESC
END
