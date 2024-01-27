CREATE PROCEDURE [Request].[spGetHandymenDetailsByRequest]
--Look up details of a provider 
	@requestId int = 0
AS

IF EXISTS (SELECT * FROM [Request].[request] WHERE [req_id] = @requestId)
BEGIN
	SELECT p.Names , p.Surname, p.DateOfBirth, p.Gender, p.PhoneNumber,
            w.work_name, w.work_registrationid

    FROM [dbo].[profile] as p,
    [Delivery].[workshop] as w 
    
    WHERE p.UserId = (SELECT [order_employeeid] FROM [Delivery].[order] WHERE [Delivery].[order].order_id = @requestId ) 
    AND w.work_Id = (SELECT [emp_workshopid] FROM [Delivery].employee WHERE [emp_profile_id] = p.UserId)
END
