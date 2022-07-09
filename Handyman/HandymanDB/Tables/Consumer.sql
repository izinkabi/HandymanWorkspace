CREATE TABLE [Customer].[Consumer] (
    [ConsumerID]       NVARCHAR (128) NOT NULL,
    [RegistrationDate] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Customer.Consumer] PRIMARY KEY CLUSTERED ([ConsumerID] ASC)
);