CREATE PROCEDURE [dbo].[spAddressLookUpBySurburb]
--Get all addresses of the given surburb
	@Surburb VARCHAR(50) 
AS
BEGIN
	SELECT a.AddressId, a.StreetName, a.PostalCode, a.City, a.Surburb, a.HouseNumber, s.Id
	FROM Profile AS p, Address AS a,ServiceProvider AS s
	WHERE s.ProfileId=p.Id AND p.HomeAddressId=a.AddressId AND a.Surburb = @Surburb;
END
