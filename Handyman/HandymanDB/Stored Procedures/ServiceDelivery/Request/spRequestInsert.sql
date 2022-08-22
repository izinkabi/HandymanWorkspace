CREATE PROCEDURE [ServiceDelivery].[spRequestInsert]
	
    @OrderId           INT,
    @ProviderServiceID INT,
    @IsDelivered      INT 
AS
BEGIN
	INSERT INTO [ServiceDelivery].[Request] (OrderId,ProviderServiceID,IsDelivered  )
    VALUES  (@OrderId,@ProviderServiceID,@IsDelivered)
END
