CREATE PROCEDURE [dbo].[spAddressLookUpByPostalCode]
--Get all addresses of the given postal code
	@PostalCode int 
AS
BEGIN 
	SET NOCOUNT ON
	SELECT *
	FROM Address
	WHERE PostalCode=@PostalCode
END