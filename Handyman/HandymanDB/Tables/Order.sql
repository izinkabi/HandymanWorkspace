CREATE TABLE [Customer].[Order] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ConsumerID]  NVARCHAR (450) NOT NULL,
    [DateCreated] DATETIME2  NOT NULL,
    [Stage]       VARCHAR (15)   NULL DEFAULT 'undone',
    [ServiceId]       INT            NOT NULL,
    [DateFinished] DATETIME2 NULL DEFAULT CURRENT_TIMESTAMP , 
    [IsAccepted] INT NULL DEFAULT 0, 
    [DateAccepted] DATETIME2 NULL, 
    CONSTRAINT [PK_Customer.Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customer.Order_Service.Job] FOREIGN KEY ([ServiceId]) REFERENCES [Service].[Service] ([Id])
);