CREATE TABLE [dbo].[Service]
(
	[ServiceId] INT NOT NULL PRIMARY KEY, 
    [type] VARCHAR(45) NULL, 
    [ServiceProviderId] INT NULL, 
    CONSTRAINT [FK_Service_ServiceProvider] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceProvider]([Id])
)
