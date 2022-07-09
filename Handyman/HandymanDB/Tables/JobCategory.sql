CREATE TABLE [Service].[JobCategory] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Service.JobCategory] PRIMARY KEY CLUSTERED ([Id] ASC)
);

