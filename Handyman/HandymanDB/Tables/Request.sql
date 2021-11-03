CREATE TABLE [dbo].[Request]
(
	[RequestId] INT NOT NULL PRIMARY KEY, 
    [consumerId] INT NOT NULL, 
    [ProvidersServiceId] INT NOT NULL, 
    [ServiceProviderId] INT NOT NULL,  
    CONSTRAINT [FK_Request_Consumer] FOREIGN KEY ([ConsumerId]) REFERENCES [Consumer]([ConsumerId]),
    CONSTRAINT [FK_Request_ProvidersServiceId] FOREIGN KEY ([ProvidersServiceId]) REFERENCES [ProvidersService]([Id])

)
