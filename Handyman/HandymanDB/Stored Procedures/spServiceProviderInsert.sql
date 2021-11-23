/*Inserting a ServiceProvider SP*/
CREATE PROCEDURE [dbo].[spServiceProviderInsert]
	
	@Id int,
	@ProfileId int,
	@CompanyName VARCHAR (50)='',
	@ProviderType VARCHAR(50) =''
	

AS
Begin
	INSERT INTO [ServiceProvider] (ProfileId,CompanyName,ProviderType)
	VALUES (@ProfileId,@CompanyName,@ProviderType)
End
