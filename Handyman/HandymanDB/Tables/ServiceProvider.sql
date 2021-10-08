CREATE TABLE [dbo].[ServiceProvider]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    
    [Name] VARCHAR(50) NOT NULL,
    [Surname] VARCHAR(50) NOT NULL,
    [HomeAddress] VARCHAR(150) NOT NULL,
    [PhoneNumber] INT NOT NULL,
    [DateOfBirth] NVARCHAR(50) NOT NULL,
    [type] VARCHAR(45) NULL, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [ServiceId] INT NOT NULL,
   
    CONSTRAINT [FK_ServiceProvider_Service] FOREIGN KEY ([ServiceId]) REFERENCES [Service]([ServiceId]),
    CONSTRAINT [FK_ServiceProvider_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
