CREATE TABLE [ServiceDelivery].[ServiceProvider] (
    [Id]               NVARCHAR (128) NOT NULL,
    [RegistrationDate] DATETIME2 (7)  NOT NULL,
    [CompanyName]      VARCHAR (50)   NOT NULL,
    [ProviderType]     VARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_ServiceDelivery.ServiceProvider] PRIMARY KEY CLUSTERED ([Id] ASC)
);

