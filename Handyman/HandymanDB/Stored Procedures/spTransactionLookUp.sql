/*Transaction Look up SP*/
CREATE PROCEDURE [dbo].[spTransactionLookUp]
	@Id int = 0
	
AS
Begin
set nocount on;
	SELECT Id, Date, ServiceId,Type,ConsumerId,RequestId
	FROM [dbo].[Transaction]
	WHERE Id = @Id
End
