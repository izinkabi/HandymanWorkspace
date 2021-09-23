CREATE PROCEDURE [dbo].[spProfileInsert]
	
	@type VARCHAR (100) = '',
	@userId VARCHAR (100)=''
AS

Begin
	INSERT INTO Profile (type,userId)
	VALUES (@type,@userId)
End
