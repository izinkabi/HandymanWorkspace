CREATE PROCEDURE [ServiceDelivery].[spServiceProviderUpdate]
	@Id		NVARCHAR,
    @CompanyName      VARCHAR,
    @ProviderType     VARCHAR
AS
BEGIN
Set nocount on
	UPDATE [ServiceDelivery].[ServiceProvider]
	SET CompanyName = @CompanyName,ProviderType=@ProviderType
	WHERE Id=@Id
END