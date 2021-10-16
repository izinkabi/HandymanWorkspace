CREATE TABLE [dbo].[Profile]
(
	[Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL,
    [Surname] VARCHAR(50) NOT NULL,
    [HomeAddress] VARCHAR(150) NULL,
    [PhoneNumber] NVARCHAR(50) NOT NULL,
    [DateOfBirth] NVARCHAR(50) NOT NULL,
    [type] VARCHAR(45) NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    
    CONSTRAINT [FK_Profile_User] FOREIGN KEY (userId) REFERENCES [User]([Id]) 
   
)
