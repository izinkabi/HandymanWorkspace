CREATE TABLE [dbo].[Request]
(
	[RequestId] INT NOT NULL PRIMARY KEY, 
    [consumerId] INT NULL, 
    [servicesId] INT NULL, 
    CONSTRAINT [FK_Request_Consumer] FOREIGN KEY ([ConsumerId]) REFERENCES [Consumer]([ConsumerId])
)
