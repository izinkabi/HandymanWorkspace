CREATE TABLE [dbo].[Consumer]
(
	[ConsumerId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [profileId] INT NULL, 
    CONSTRAINT [FK_Consumer_Profile] FOREIGN KEY ([profileId]) REFERENCES [Profile]([Id])
)
