CREATE PROCEDURE [dbo].[spProfileUpDate]
--Updating the profile store procedure
	@Id int=0,
	@name VARCHAR (50)='',
	@surname VARCHAR(50) ='',
	@PhoneNumber NVARCHAR (50)='',
	@dateofbirth NVARCHAR(100) 
AS
BEGIN
	UPDATE [dbo].[Profile]
	SET Name = @name,Surname=@surname,DateOfBirth=@dateofbirth,Phonenumber=@phonenumber
	WHERE Id=@Id
END
