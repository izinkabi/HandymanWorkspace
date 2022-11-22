CREATE PROCEDURE [dbo].[spProfileInsert]
	@Names NVARCHAR(200),
    @Surname NVARCHAR(200),
    @EmailAddress NVARCHAR(100),
    @PhoneNumber NVARCHAR(100),
    @AddressId INT,
    @DateOfBirth DATETIME2(7),
    @userId NVARCHAR(450)
AS
BEGIN
	INSERT INTO [dbo].[Profile] ([Names],[Surname],[EmailAddress],[AddressId],[DateOfBirth],[userId],[PhoneNumber])
    VALUES (@Names, @Surname, @EmailAddress , @AddressId, @DateOfBirth, @userId, @PhoneNumber)
END
