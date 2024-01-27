CREATE PROCEDURE [Delivery].[spWorShopServices_LookUp]
--Look up for workshop services
	@workShopRegId int = 0
AS
BEGIN
	SELECT c.title,c.[description],c.originalServiceId,c.basePrice,c.imageUrl,c.Id
    FROM [Delivery].[WorkShopServices] as w , [Service].[customService] as c
    WHERE w.[workShopRegId] = @workShopRegId AND w.[customServiceId] = c.Id
END
