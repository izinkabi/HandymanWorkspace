/*Looking for ServiceProvider with a given ID*/
CREATE PROCEDURE [dbo].[spServiceProviderLookUp]
	@Id int = 0
AS
Begin
set nocount on;
	SELECT *
	FROM dbo.ServiceProvider
	WHERE Id = @Id
End
