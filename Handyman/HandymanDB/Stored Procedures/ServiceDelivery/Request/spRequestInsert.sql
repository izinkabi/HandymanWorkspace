CREATE PROCEDURE [ServiceDelivery].[spRequestInsert]
	
    @OrderId           INT,
    @ServiceId INT,
    @IsDelivered      INT 
AS
BEGIN
	INSERT INTO [ServiceDelivery].[Request] (OrderId,ServiceId,IsDelivered  )
    VALUES  (@OrderId,@ServiceId,@IsDelivered)
END
