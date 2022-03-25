CREATE PROCEDURE [dbo].[spAddressInsert]
	@PostalCode INT,
	@StreetName VARCHAR(50),
	@Surburb VARCHAR(50),
	@City VARCHAR(100),
	@HouseNumber INT
AS
BEGIN
	INSERT INTO Address (PostalCode, StreetName , Surburb , City , HouseNumber)
	VALUES(@PostalCode,@StreetName,@Surburb,@City,@HouseNumber)
END
