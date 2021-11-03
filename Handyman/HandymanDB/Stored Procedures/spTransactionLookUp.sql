/*Transaction Look up SP*/
CREATE PROCEDURE [dbo].[spTransactionLookUp]
	@Id int 
	
AS
Begin
set nocount on;
	SELECT *
	FROM [dbo].[Transaction]
	WHERE Id = @Id
End
