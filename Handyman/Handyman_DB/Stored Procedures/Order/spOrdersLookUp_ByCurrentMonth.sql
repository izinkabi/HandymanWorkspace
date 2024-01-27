CREATE PROCEDURE [Delivery].[spOrdersLookUp_ByCurrentMonth]
	@employeeId NVARCHAR (450)
    --orders of the current month
AS
BEGIN
	SELECT [order_id],[order_datecreated],[order_employeeid],[order_status],[order_progress],[order_requestId]
    FROM [Delivery].[order]
    WHERE DATEPART(month,[order_datecreated]) = DATEPART(MONTH, CAST(GETDATE() AS DATE))
    AND [order_employeeid] = @employeeId 
    GROUP BY [order_id],[order_id],[order_datecreated],[order_employeeid],[order_status],[order_progress],[order_requestId]
    ORDER BY [order_datecreated] DESC
END
