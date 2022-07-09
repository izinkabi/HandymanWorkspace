CREATE TABLE [ServiceDelivery].[ProviderService] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ServiceProviderId] NVARCHAR (128) NOT NULL,
    [JobId]             INT            NOT NULL,
    CONSTRAINT [PK_ServiceDelivery.ProviderService] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceDelivery.ProviderService_Service.Job] FOREIGN KEY ([JobId]) REFERENCES [Service].[Job] ([JobID]),
    CONSTRAINT [FK_ServiceDelivery.ProviderService_ServiceDelivery.ServiceProvider] FOREIGN KEY ([ServiceProviderId]) REFERENCES [ServiceDelivery].[ServiceProvider] ([Id])
);

