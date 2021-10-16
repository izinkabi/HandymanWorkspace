/*Looking for ServiceProvider with a given ID*/
CREATE PROCEDURE [dbo].[spServiceProviderLookUp]
	@UserId nvarchar(128) = '' 
AS
Begin
set nocount on;
	SELECT Id,Name,Surname,HomeAddress,PhoneNumber
	FROM ServiceProvider
	WHERE UserId = @UserId
End
