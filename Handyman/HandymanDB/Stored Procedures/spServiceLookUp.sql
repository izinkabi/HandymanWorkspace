/*Looking for a Service of a given ID SP*/
CREATE PROCEDURE [dbo].[spServiceLookUp]
	@Id int = 0
AS
Begin 
set nocount on;
	SELECT ServiceId,type,ServiceProviderId
	FROM dbo.Service
	WHERE ServiceId = @Id
End
