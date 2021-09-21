/*Consumer Look up SP */
CREATE PROCEDURE [dbo].[spConsumerLookUp]
	@Id int = 0
	
AS
Begin
set nocount on;
	SELECT ConsumerId,ProfileId
	FROM dbo.Consumer
	WHERE ConsumerId = @Id
End
