/*Looking for ServiceProvider with a given ID*/
CREATE PROCEDURE [dbo].[spServiceProviderLookUp]
	@Id int = 0
AS
Begin
	SELECT Id, RequestId, ProfileId
	FROM dbo.ServiceProvider
	WHERE Id = @Id
End
