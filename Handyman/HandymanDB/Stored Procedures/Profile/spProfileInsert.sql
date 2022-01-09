CREATE PROCEDURE [dbo].[spProfileInsert]
	
	
	@UserId NVARCHAR (150)='',
	@name VARCHAR (50)='',
	@surname VARCHAR(50) ='',
	@PhoneNumber NVARCHAR (50)='',
	@AddressId INT,
	@dateofbirth NVARCHAR(100) 
	

AS

Begin
	INSERT INTO Profile (userId,Name,Surname,HomeAddressId,DateOfBirth,Phonenumber)
	VALUES (@userId,@name,@surname,@AddressId,@dateofbirth,@PhoneNumber)
End
