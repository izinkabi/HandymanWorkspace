CREATE PROCEDURE Customer.spConsumerLookUpById 
    @ConsumerID       NVARCHAR
AS 
	BEGIN
		SET NOCOUNT ON;  
		SELECT ConsumerId,RegistrationDate  
		FROM Customer.Consumer 
		WHERE ConsumerID = @ConsumerID 
	END