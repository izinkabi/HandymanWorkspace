CREATE TABLE [Customer].[Order] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ConsumerID]  NVARCHAR (128) NOT NULL,
    [DateCreated] DATETIME2 (7)  NOT NULL,
    [Stage]       VARCHAR (15)   NOT NULL,
    [JobId]       INT            NOT NULL,
    CONSTRAINT [PK_Customer.Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customer.Order_Customer.Consumer] FOREIGN KEY ([ConsumerID]) REFERENCES [Customer].[Consumer] ([ConsumerID]),
    CONSTRAINT [FK_Customer.Order_Service.Job] FOREIGN KEY ([JobId]) REFERENCES [Service].[Job] ([JobID])
);