CREATE PROCEDURE [dbo].[spAddressUpdate]
--Address update stored procedure
	@PostalCode INT,
	@StreetName VARCHAR(50),
	@Surburb VARCHAR(50),
	@City VARCHAR(100),
	@HouseNumber INT,
	@Id INT
AS
BEGIN
	UPDATE [dbo].[Address]
	SET PostalCode = @PostalCode, StreetName=@StreetName , Surburb = Surburb , City = @City , HouseNumber = @HouseNumber
	WHERE AddressId = @Id
END
