CREATE PROCEDURE [dbo].[spTransactionInsert]
	@Date Date ,
	@ServiceId int = 0,
	@type VARCHAR (45) = '',
	@ConsumerId int = 0,
	@RequestId int = 0
AS
Begin

	INSERT INTO [Transaction] (Date,ServiceId,Type,ConsumerId,RequestId)
	VALUES (@Date,@ServiceId,@type,@ConsumerId,@RequestId)
End
