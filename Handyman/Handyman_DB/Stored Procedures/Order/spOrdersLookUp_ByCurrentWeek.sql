CREATE PROCEDURE [Delivery].[spOrdersLookUp_ByCurrentWeek]
    @employeeId NVARCHAR(450)
    --member's weekly work
AS
BEGIN
	SELECT [order_id],[order_datecreated],[order_employeeid],[order_status],[order_progress],[order_requestId]
   
    FROM [Delivery].[order]
    WHERE DATEPART(week,[order_datecreated]) = DATEPART(week, CAST(GETDATE() AS DATE))
    AND [order_employeeid]= @employeeId
    GROUP BY [order_id],[order_id],[order_datecreated],[order_employeeid],[order_status],[order_progress],[order_requestId]
    ORDER BY [order_datecreated] DESC
END
