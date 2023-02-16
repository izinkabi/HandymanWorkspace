CREATE PROCEDURE [Delivery].[spRequestsLookUp_ByCurrentMonth]
	@employeeId NVARCHAR (450)
    --requests of the current month
AS
BEGIN
	SELECT [req_id],[req_datecreated],[req_employeeid],[req_status],[req_progress],[req_orderid]
    FROM [Delivery].[request]
    WHERE DATEPART(month,[req_datecreated]) = DATEPART(MONTH, CAST(GETDATE() AS DATE))
    AND [req_employeeid] = '664d8943-6ee4-457f-a6f5-89c73bf8598d' 
    GROUP BY [req_id],[req_id],[req_datecreated],[req_employeeid],[req_status],[req_progress],[req_orderid]
    ORDER BY [req_datecreated] DESC
END
