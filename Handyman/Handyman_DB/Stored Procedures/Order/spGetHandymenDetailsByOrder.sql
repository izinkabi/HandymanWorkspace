CREATE PROCEDURE [Request].[spGetHandymenDetailsByOrder]
--Look up details of a provider 
	@orderId int = 0
AS

IF EXISTS (SELECT * FROM [Delivery].[request] WHERE [req_orderid] = @orderId)
BEGIN
	SELECT p.Names , p.Surname, p.DateOfBirth, p.Gender, p.PhoneNumber,
            b.bus_name,b.bus_registrationid

    FROM [dbo].[profile] as p,
    [Delivery].[business] as b 
    
    WHERE p.UserId = (SELECT [req_employeeid] FROM [Delivery].[request] WHERE [Delivery].[request].req_orderid = @orderId ) 
    AND b.bus_Id = (SELECT [emp_businessid] FROM [Delivery].employees WHERE [emp_profile_id] = p.UserId)
END
