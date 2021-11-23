CREATE TABLE [dbo].[Request]
(
	[RequestId] INT NOT NULL PRIMARY KEY, 
    [ConsumerId] INT NOT NULL, 
    [ProvidersServiceId] INT NOT NULL, 
    CONSTRAINT [FK_Request_Consumer] FOREIGN KEY ([ConsumerId]) REFERENCES [Consumer]([Id]),
    CONSTRAINT [FK_Request_ProvidersServiceId] FOREIGN KEY ([ProvidersServiceId]) REFERENCES [ProvidersService]([Id])

)
