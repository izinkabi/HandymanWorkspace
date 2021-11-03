CREATE PROCEDURE [dbo].[spProfileInsert]
	
	
	@UserId NVARCHAR (150)='',
	@name VARCHAR (50)='',
	@surname VARCHAR(50) ='',
	@PhoneNumber NVARCHAR (50)='',
	@homeAddressId VARCHAR(150)='',
	@dateofbirth NVARCHAR(100) 
	

AS

Begin
	INSERT INTO Profile (userId,Name,Surname,HomeAddressId,DateOfBirth,Phonenumber)
	VALUES (@userId,@name,@surname,@homeAddressId,@dateofbirth,@PhoneNumber)
End
