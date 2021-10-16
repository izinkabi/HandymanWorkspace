CREATE PROCEDURE [dbo].[spProfileInsert]
	
	@type VARCHAR (100) = '',
	@UserId NVARCHAR (150)='',
	@name VARCHAR (50)='',
	@surname VARCHAR(50) ='',
	@PhoneNumber NVARCHAR (50)='',
	@homeAddress VARCHAR(150)='',
	@dateofbirth NVARCHAR(100) 
	

AS

Begin
	INSERT INTO Profile (type,userId,Name,Surname,HomeAddress,DateOfBirth,Phonenumber)
	VALUES (@type,@userId,@name,@surname,@homeAddress,@dateofbirth,@PhoneNumber)
End
