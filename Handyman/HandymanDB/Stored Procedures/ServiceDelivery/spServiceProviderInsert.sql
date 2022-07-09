CREATE PROCEDURE [ServiceDelivery].[spServiceProviderInsert]
	@Id               NVARCHAR,
    @RegistrationDate DATETIME2,
    @CompanyName      VARCHAR,
    @ProviderType     VARCHAR
AS
Begin

	INSERT INTO [ServiceDelivery].[ServiceProvider] (Id,RegistrationDate,CompanyName,ProviderType)
	VALUES (@Id,@RegistrationDate,@CompanyName,@ProviderType)
End
