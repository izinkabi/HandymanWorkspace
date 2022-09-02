CREATE PROCEDURE [ServiceDelivery].[spRequestInsert]
	--create a new request using the order details
    @OrderId     INT,
    @ServiceProviderId NVARCHAR (450),
    @ServiceId INT,
    @IsDelivered   INT, 
    @DateAccepted DATETIME2
AS
BEGIN
	INSERT INTO [ServiceDelivery].[Request] (OrderId,ServiceId,IsDelivered,ServiceProviderId,DateAccepted )
    VALUES  (@OrderId,@ServiceId,@IsDelivered,@ServiceProviderId,@DateAccepted)
END
