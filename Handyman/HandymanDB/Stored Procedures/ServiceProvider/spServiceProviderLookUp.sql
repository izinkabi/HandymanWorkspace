/*Looking for ServiceProvider with a given ID*/
CREATE PROCEDURE [dbo].[spServiceProviderLookUp]
	@Id int  
AS
Begin
set nocount on;
	SELECT *
	FROM ServiceProvider
	WHERE Id = @Id
End
