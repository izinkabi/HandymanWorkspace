CREATE PROCEDURE [dbo].[spTransactionInsert]
	@RequestId int,
	@DateCreated DATETIME
AS
Begin

	INSERT INTO [Transaction] (DateCreated,RequestId)
	VALUES (@DateCreated,@RequestId)
End
