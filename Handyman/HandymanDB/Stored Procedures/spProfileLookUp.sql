/*Profile Look Up*/
CREATE PROCEDURE [dbo].[spProfileLookUp]
	@UserId nvarchar(128)
AS
BEGIN
/*Alias of Address and Profile tables to form a profile model */
	SELECT p.UserId, p.Id, p.Name,p.Surname,p.DateOfBirth, p.PhoneNumber, p.HomeAddressId, a.AddressId, a.StreetName, a.PostalCode, a.City, a.Surburb, a.HouseNumber
	FROM Profile AS p, Address AS a
	WHERE p.UserId=@UserId AND p.HomeAddressId=a.AddressId;
End
