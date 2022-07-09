CREATE PROCEDURE [Customer].[spConsumerInsert]
	@ConsumerID       NVARCHAR,
    @RegistrationDate DATETIME2
AS
Begin

	INSERT INTO [Customer].Consumer (ConsumerID,RegistrationDate)
	VALUES (@ConsumerID,@RegistrationDate)
End