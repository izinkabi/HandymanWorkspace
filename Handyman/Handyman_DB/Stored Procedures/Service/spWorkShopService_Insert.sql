CREATE PROCEDURE [Delivery].[spWorkShopService_Insert]
--Insert new workshop-service

 @workShopRegId INT =0,
 @customServiceId INT = 0
AS
IF NOT EXISTS (SELECT * FROM [Delivery].[WorkShopServices] 
WHERE [workShopRegId] = @workShopRegId AND [customServiceId] =@customServiceId)
BEGIN
	INSERT INTO [Delivery].[WorkShopServices] ([customServiceId],[workShopRegId])
    VALUES (@customServiceId,@workShopRegId)
END
