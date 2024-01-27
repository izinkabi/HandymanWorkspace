CREATE PROCEDURE [Delivery].[spOrderLookUpByProvider]
	@memberId NVARCHAR(450)
AS
BEGIN
	SELECT [order_id],[order_datecreated],[order_employeeid],[order_status],[order_progress],[order_requestId]
    FROM [Delivery].[order]
    WHERE [order_employeeid] = @memberId
    GROUP BY [order_id],[order_id],[order_datecreated],[order_employeeid],[order_status],[order_progress],[order_requestId]
    ORDER BY [order_datecreated] DESC
END
