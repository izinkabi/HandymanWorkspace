CREATE PROCEDURE [Delivery].[spBusiness_Registration_LookUp]
	@businessId int = 0
AS
BEGIN
SET NOCOUNT ON
	SELECT *
    FROM [Delivery].[business] b
    INNER JOIN [Delivery].[registration] r ON r.[reg_Id] = b.bus_registrationid
    INNER JOIN [Delivery].[address] a ON a.[add_Id] = b.[bus_addressid]
END
