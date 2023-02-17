﻿CREATE PROCEDURE [Delivery].[spRequestLookUpByProvider]
	@providerId NVARCHAR(450)
AS
BEGIN
	SELECT [req_id],[req_datecreated],[req_employeeid],[req_status],[req_progress],[req_orderid]
    FROM [Delivery].[request]
    WHERE [req_employeeid] = @providerId
    GROUP BY [req_id],[req_id],[req_datecreated],[req_employeeid],[req_status],[req_progress],[req_orderid]
    ORDER BY [req_datecreated] DESC
END
