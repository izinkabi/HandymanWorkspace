CREATE PROCEDURE [dbo].[spAddressLookUpByCity]
--Get all addresses of the given city
	@City Varchar(100)
AS
BEGIN
SET NOCOUNT ON
	SELECT a.AddressId, a.StreetName, a.PostalCode, a.City, a.Surburb, a.HouseNumber, s.Id
	FROM Profile AS p, Address AS a,ServiceProvider AS s
	WHERE s.ProfileId=p.Id AND p.HomeAddressId=a.AddressId AND a.City = @City;
END
