CREATE TABLE [dbo].[ServiceProvider]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [RequestId] INT NULL, 
    [ProfileId] INT NULL, 
    CONSTRAINT [FK_ServiceProvider_Profile] FOREIGN KEY ([ProfileId]) REFERENCES [Profile]([Id]), 
    CONSTRAINT [FK_ServiceProvider_Request] FOREIGN KEY ([RequestId]) REFERENCES [Request]([RequestId])
)
