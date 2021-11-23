/*Consumer Look up SP */
CREATE PROCEDURE [dbo].[spConsumerLookUp]
	@Id int = 0
	
AS
Begin
set nocount on;
	SELECT Id,ProfileId
	FROM dbo.Consumer
	WHERE Id = @Id
End
