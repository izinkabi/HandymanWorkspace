/*Inserting a ServiceProvider SP*/
CREATE PROCEDURE [dbo].[spServiceProviderInsert]
	
	@Id int,
	@UserId NVARCHAR (128)='',
	@name VARCHAR (50)='',
	@surname VARCHAR(50) ='',
	@PhoneNumber NVARCHAR(150),
	@homeAddress NVARCHAR(150)='',
	@dateofbirth NVARCHAR(50) =''
	

AS
Begin
	INSERT INTO [ServiceProvider] (Name,surname,HomeAddress,PhoneNumber,DateOfBirth,UserId)
	VALUES (@Name,@Surname,@HomeAddress,@PhoneNumber,@DateOfBirth,@UserId)
End
