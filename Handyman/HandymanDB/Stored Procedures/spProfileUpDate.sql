CREATE PROCEDURE [dbo].[spProfileUpDate]
--Updating the profile store procedure
	@UserId NVARCHAR(128)='',
	@name VARCHAR (50)='',
	@surname VARCHAR(50) ='',
	@PhoneNumber NVARCHAR (50)='',
	@dateofbirth NVARCHAR(100) 
AS
BEGIN
	UPDATE [dbo].[Profile]
	SET Name = @name,Surname=@surname,DateOfBirth=@dateofbirth,Phonenumber=@phonenumber
	WHERE UserId=@UserId
END
