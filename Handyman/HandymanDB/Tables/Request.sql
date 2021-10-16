CREATE TABLE [dbo].[Request]
(
	[RequestId] INT NOT NULL PRIMARY KEY, 
    [consumerId] INT NOT NULL, 
    [servicesId] INT NOT NULL, 
    [ServiceProviderId] INT NOT NULL,  
    CONSTRAINT [FK_Request_Consumer] FOREIGN KEY ([ConsumerId]) REFERENCES [Consumer]([ConsumerId]),
    CONSTRAINT [FK_Request_ServiceProviderId] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceProvider]([Id])

)
