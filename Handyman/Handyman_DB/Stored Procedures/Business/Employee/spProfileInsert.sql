CREATE PROCEDURE [dbo].[spProfileInsert]
	@Names NVARCHAR(200),
    @Surname NVARCHAR(200),
    @EmailAddress NVARCHAR(100),
    @PhoneNumber NVARCHAR(100),
    @AddressId INT,
    @DateOfBirth DATETIME2(7),
    @userId NVARCHAR(450),
    @profileGender NVARCHAR(50)

     
     
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM [dbo].[Profile] WHERE [UserId] = @userId)
    INSERT INTO [dbo].[Profile] ([Names],[Surname],[EmailAddress],[AddressId],[DateOfBirth],[userId],[PhoneNumber],[Gender])
    VALUES (@Names, @Surname, @EmailAddress , @AddressId, @DateOfBirth, @userId, @PhoneNumber,@profileGender)
END
