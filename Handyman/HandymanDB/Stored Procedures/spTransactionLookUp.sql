/*Transaction Look up SP*/
CREATE PROCEDURE [dbo].[spTransactionLookUp]
	@Id int = 0
	
AS
Begin

	SELECT Id, Date, ServiceId,type,ConsumerId,RequestId
	FROM [dbo].[Transaction]
	WHERE Id = @Id
End
