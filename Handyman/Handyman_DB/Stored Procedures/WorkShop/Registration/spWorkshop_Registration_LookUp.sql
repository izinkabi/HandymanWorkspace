CREATE PROCEDURE [Delivery].[spWorhshop_Registration_LookUp]
--Looking up for the WS registration
	@workshopId int = 0
AS
BEGIN
SET NOCOUNT ON
	SELECT *
    FROM [Delivery].[workshop] w
    INNER JOIN [Delivery].[registration] r ON r.[reg_Id] = w.[work_registrationid]
    INNER JOIN [Delivery].[address] a ON a.[add_Id] = w.[work_addressid]
    WHERE w.[work_Id] = @workshopId
END
