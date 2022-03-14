CREATE PROCEDURE [dbo].[spAddressLookUpByCity]
--Get all addresses of the given city
	@City Varchar(100)
AS
BEGIN
SET NOCOUNT ON
	SELECT *
	FROM Address
	WHERE City = @City
END
