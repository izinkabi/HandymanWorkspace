CREATE TABLE [ServiceDelivery].[Request] (
    [Id]                INT IDENTITY (1, 1) NOT NULL,
    [OrderId]           INT NOT NULL,
    [ProviderServiceID] INT NOT NULL,
    [IsDelivered]       INT NOT NULL,
    CONSTRAINT [PK_ServiceDelivery.Request] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceDelivery.Request_Customer.Order] FOREIGN KEY ([OrderId]) REFERENCES [Customer].[Order] ([Id]),
    CONSTRAINT [FK_ServiceDelivery.Request_ServiceDelivery.ServiceProvider] FOREIGN KEY ([ProviderServiceID]) REFERENCES [ServiceDelivery].[ProviderService] ([Id])
);

