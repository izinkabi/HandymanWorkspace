CREATE TABLE [dbo].[Profile]
(
    [Names] NVARCHAR(200) NOT NULL, 
    [Surname] NVARCHAR(200) NOT NULL, 
    [EmailAddress] NVARCHAR(100) NOT NULL, 
    [AddressId] INT NOT NULL, 
    [DateOfBirth] DATETIME2 NOT NULL, 
    [UserId] NVARCHAR(450) NOT NULL PRIMARY KEY ,
    [Gender] NVARCHAR(50) NOT NULL, 
    [PhoneNumber] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Profile_userId] CHECK (1 = 1), 
   -- CONSTRAINT [FK_Profile_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers]([Id])

    
)
