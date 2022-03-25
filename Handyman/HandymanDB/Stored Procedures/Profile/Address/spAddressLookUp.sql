CREATE PROCEDURE [dbo].[spAddressLookUp]
	@HouseNumber int,
	@StreetName varchar(50)
AS
BEGIN
	SELECT *
	FROM Address 
	WHERE HouseNumber=@HouseNumber AND [StreetName]=@StreetName
	
END