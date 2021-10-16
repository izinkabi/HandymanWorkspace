CREATE TABLE [dbo].[ProvidersService]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ServiceProviderId] INT NOT NULL,
	[ServiceId] INT NOT NULL,
	CONSTRAINT [FK_ProvidersService_ServiceProvider] FOREIGN KEY (ServiceProviderId) REFERENCES [ServiceProvider]([Id]),
	CONSTRAINT [FK_ProvidersService_Service] FOREIGN KEY (ServiceId) REFERENCES [Service](ServiceId)
)
