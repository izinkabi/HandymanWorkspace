CREATE PROCEDURE [Delivery].[spWorkShopService_Delete]
--Remove a workshop-service
	@workShopServiceId int = 0,
    @workShopRegId int = 0
AS
IF EXISTS (SELECT * FROM [Delivery].[WorkShopServices] WHERE [workShopRegId] = @workShopRegId)
BEGIN
    DELETE FROM [Delivery].[WorkShopServices]
    WHERE [customServiceId] = @workShopServiceId
END
