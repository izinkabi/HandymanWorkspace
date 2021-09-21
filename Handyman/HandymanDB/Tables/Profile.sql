CREATE TABLE [dbo].[Profile]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [type] VARCHAR(45) NULL, 
    [userId] VARCHAR(45) NULL, 
    [ActivationId] INT NULL, 
    CONSTRAINT [FK_Profile_User] FOREIGN KEY (userId) REFERENCES [User]([userId]), 
    CONSTRAINT [FK_Profile_Activation] FOREIGN KEY (ActivationId) REFERENCES [Activation]([ActivationId]) 
)
