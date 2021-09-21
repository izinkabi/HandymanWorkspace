CREATE PROCEDURE [dbo].[spProfileInsert]
	@Id int = 0,
	@type VARCHAR (100) = '',
	@ActivationId int = 0,
	@userId VARCHAR (100)=''
AS

Begin
	INSERT INTO Profile (type,ActivationId,userId)
	VALUES (@type,@ActivationId,@userId)
End
