/*Looking for ServiceProvider with a given ID*/
CREATE PROCEDURE [dbo].[spServiceProviderLookUp]
	@ProfileId int  
AS
Begin
set nocount on;
	SELECT *
	FROM ServiceProvider
	WHERE ProfileId = @ProfileId
End
