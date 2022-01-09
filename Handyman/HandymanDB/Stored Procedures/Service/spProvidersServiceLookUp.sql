CREATE PROCEDURE [dbo].[spProvidersServiceLookUp]
	@ServiceProviderId int = 0
	
AS
Begin
set nocount on;
	SELECT *
	FROM ProvidersService
	WHERE ServiceProviderId = @ServiceProviderId
End
