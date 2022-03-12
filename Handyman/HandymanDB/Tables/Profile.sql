
CREATE TABLE [dbo].[Profile]
(
	[Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL,
    [Surname] VARCHAR(50) NOT NULL,
    [HomeAddressId] INT NOT NULL,
    [PhoneNumber] NVARCHAR(50) NOT NULL,
    [DateOfBirth] NVARCHAR(50) NOT NULL,
    [UserId] NVARCHAR(128) NOT NULL, 
    
    CONSTRAINT [FK_Profile_User] FOREIGN KEY (userId) REFERENCES [User]([Id]),
    CONSTRAINT [FK_Profile_Address] FOREIGN KEY ([HomeAddressId]) REFERENCES [Address]([AddressId]) 
   
)
