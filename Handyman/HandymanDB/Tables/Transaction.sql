CREATE TABLE [dbo].[Transaction]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [DateCreated] DATETIME NOT NULL, 
    [RequestId] INT NOT NULL, 
    CONSTRAINT [FK_Transaction_Request] FOREIGN KEY ([RequestId]) REFERENCES [Request]([Id])
)
