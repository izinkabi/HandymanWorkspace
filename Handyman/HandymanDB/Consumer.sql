CREATE TABLE [dbo].[Consumer]
(
	[ConsumerId] INT NOT NULL PRIMARY KEY, 
    [profileId] INT NULL, 
    CONSTRAINT [FK_Consumer_Profile] FOREIGN KEY ([profileId]) REFERENCES [Profile]([Id])
)
