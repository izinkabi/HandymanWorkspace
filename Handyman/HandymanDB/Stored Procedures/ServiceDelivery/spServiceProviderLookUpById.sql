CREATE PROCEDURE [ServiceDelivery].[spServiceProviderLookUpById]
	@Id               NVARCHAR 
AS
Begin

	SELECT Id,RegistrationDate,CompanyName,ProviderType
	FROM  [ServiceDelivery].[ServiceProvider] 
	WHERE Id = @Id
End