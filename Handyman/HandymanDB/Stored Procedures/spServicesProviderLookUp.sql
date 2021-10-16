CREATE PROCEDURE [dbo].[spServicesProviderLookUp]
	@ServiceId int = 0

AS
Begin
set nocount on;
	SELECT *
	FROM ProvidersService
	WHERE ServiceId = @ServiceId
End
