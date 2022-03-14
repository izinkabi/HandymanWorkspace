CREATE PROCEDURE [dbo].[spAddressLookUpBySurburb]
--Get all addresses of the given surburb
	@Surburb VARCHAR(50) 
AS
BEGIN
	SELECT *
	FROM Address
	WHERE Surburb = @Surburb
END
