CREATE TABLE [dbo].[Consumer]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ProfileId] INT NOT NULL, 
    [Activation] INT NOT NULL, 
    CONSTRAINT [FK_Consumer_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [Profile]([Id])
)
