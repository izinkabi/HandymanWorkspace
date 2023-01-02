CREATE PROCEDURE [Delivery].[spRequestInsert]
	
	@req_datecreated datetime2 (7) ,
	@req_status nchar(10) ,
	@req_progress nchar(10) , 
    @req_employeeid NVARCHAR (450) , 
    @req_orderid INT 
AS
    IF NOT EXISTS (SELECT * FROM  [Delivery].[request] WHERE [req_orderid] = @req_orderid AND [req_employeeid] = @req_employeeid)
BEGIN
	INSERT INTO [Delivery].[request] (req_datecreated, req_status, 
                                        req_progress, req_employeeid, req_orderid)
    VALUES (@req_datecreated, @req_status, @req_progress,
            @req_employeeid,@req_orderid)
END
