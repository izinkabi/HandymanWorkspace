CREATE PROCEDURE [dbo].[spUserInsert]
	@Id NVARCHAR (128) = '',
	@username NVARCHAR (256) = '',
	@Email NVARCHAR (256) = ''
	
AS
Begin
	INSERT INTO [User] (Id,username,email)
	VALUES (@Id,@username,@Email)
End
