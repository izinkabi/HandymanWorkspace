CREATE PROCEDURE [dbo].[spServiceProviderUpdate]
	@ProfileId int,
	@CompanyName VARCHAR (50)='',
	@ProviderType VARCHAR(50) =''
AS
BEGIN
Set nocount on
	UPDATE [dbo].[ServiceProvider]
	SET CompanyName = @CompanyName,ProviderType=@ProviderType
	WHERE ProfileId=@ProfileId
END
