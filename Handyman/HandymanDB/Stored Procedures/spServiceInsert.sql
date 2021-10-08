CREATE PROCEDURE [dbo].[spServiceInsert]
	
	@name VARCHAR (50)
AS
Begin
	INSERT INTO Service (Name)
	VALUES (@Name)
End
