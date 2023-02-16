CREATE PROCEDURE [Delivery].[spRequestsLookUp_ByCurrentWeek]
    @employeeId NVARCHAR(450)
    --provider's weekly work
AS
BEGIN
	SELECT [req_id],[req_datecreated],[req_employeeid],[req_status],[req_progress],[req_orderid]
   
    FROM [Delivery].[request]
    WHERE DATEPART(week,[req_datecreated]) = DATEPART(week, CAST(GETDATE() AS DATE))
    AND [req_employeeid]= @employeeId
    GROUP BY [req_id],[req_id],[req_datecreated],[req_employeeid],[req_status],[req_progress],[req_orderid]
    ORDER BY [req_datecreated] DESC
END
