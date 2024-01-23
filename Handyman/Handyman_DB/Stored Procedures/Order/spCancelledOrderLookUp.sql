CREATE PROCEDURE [Delivery].[spCancelledOrderLookUp]
--Cancel the request
	@employeeId NVARCHAR(450)
AS
BEGIN
	SELECT *
    FROM [Delivery].[order] 
    WHERE  [order].[order_status] = 11 AND [order_employeeid] = @employeeId
    ORDER BY [order_datecreated] DESC
END
