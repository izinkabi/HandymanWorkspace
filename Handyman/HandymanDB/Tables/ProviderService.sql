CREATE TABLE [ServiceDelivery].[ProviderService] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ServiceProviderId] NVARCHAR (128) NOT NULL,
    [ServiceId]             INT            NOT NULL,
    CONSTRAINT [PK_ServiceDelivery.ProviderService] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceDelivery.ProviderService_Service.Service] FOREIGN KEY ([ServiceId]) REFERENCES [Service].[Service] ([Id]),
    CONSTRAINT [FK_ServiceDelivery.ProviderService_ServiceDelivery.ServiceProvider] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceDelivery].[ServiceProvider] ([Id])
);

