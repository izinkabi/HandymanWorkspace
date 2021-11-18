CREATE PROCEDURE [dbo].[spAddressLookUp]
	@HouseNumber int,
	@StreetName varchar(50)
AS
BEGIN
	SELECT *
	FROM Address 
	WHERE [House Number]=@HouseNumber AND [Street Name]=@StreetName
	
END
