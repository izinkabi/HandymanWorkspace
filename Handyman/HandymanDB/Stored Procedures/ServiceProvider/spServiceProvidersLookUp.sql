
CREATE PROCEDURE [dbo].[spServiceProvidersLookUp]
	

	@ProfileId int,
	@CompanyName VARCHAR (50)='',
	@ProviderType VARCHAR(50) =''
	
	

AS
Begin
	INSERT INTO [ServiceProvider] (CompanyName,ProfileId,ProviderType)
	VALUES (@ProfileId,@CompanyName,@ProviderType)
End
