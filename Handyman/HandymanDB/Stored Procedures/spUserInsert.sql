CREATE PROCEDURE [dbo].[spUserInsert]
	@UserId VARCHAR (128) = '',
	@username VARCHAR (256) = '',
	@Email VARCHAR (256) = '',
	@Password VARCHAR (100)
AS
Begin
	INSERT INTO [User] (UserId,username,Email,password)
	VALUES (@UserId,@username,@Email,@Password)
End
