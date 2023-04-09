CREATE PROCEDURE [Service].[spCustomService_Insert]
--Insert a custom service
    @Id int = 0 ,
	@title NVARCHAR(50),
	@description NVARCHAR(100),
    @originalServiceId int,
    @imageUrl NVARCHAR(2000),
    @basePrice float
AS
IF EXISTS (SELECT * FROM [service] WHERE [serv_id] = @originalServiceId)
BEGIN
	INSERT INTO [Service].[customService] ([title],[description],[originalServiceId],[imageUrl],[basePrice])
    VALUES(@title,@description,@originalServiceId,@imageUrl,@basePrice)

    DECLARE @CustomServiceId INT = SCOPE_IDENTITY();

    SELECT [Id] FROM [Service].[customService] WHERE [Id] = @CustomServiceId
END

