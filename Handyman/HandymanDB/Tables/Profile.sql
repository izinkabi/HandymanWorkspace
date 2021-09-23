CREATE TABLE [dbo].[Profile]
(
	[Id] INT DEFAULT ((1)) NOT NULL PRIMARY KEY, 
    [type] VARCHAR(45) NULL, 
    [userId] VARCHAR(45) NULL, 
    
    CONSTRAINT [FK_Profile_User] FOREIGN KEY (userId) REFERENCES [User]([userId]), 
   
)
