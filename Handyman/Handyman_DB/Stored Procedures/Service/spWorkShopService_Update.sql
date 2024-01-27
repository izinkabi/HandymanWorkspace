CREATE PROCEDURE [Delivery].[spWorkShopService_Update]
--Update the workshop service, which is a custom service
	@Id int = 0 ,
	@title NVARCHAR(50),
	@description NVARCHAR(100),
    @originalServiceId int,
    @imageUrl NVARCHAR(2000),
    @basePrice float
AS
IF EXISTS (SELECT * FROM [Service].[customService] WHERE [Id] = @Id)
BEGIN
	UPDATE [Service].[customService]
    SET [title] = @title,[description] = @description,
    [originalServiceId] = @originalServiceId,[imageUrl]=@imageUrl,
    [basePrice] = @basePrice

    WHERE [Id] = @Id
END
