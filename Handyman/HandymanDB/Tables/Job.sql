CREATE TABLE [Service].[Job] (
    [JobID]          INT           IDENTITY (1, 1) NOT NULL,
    [JobPosition]    VARCHAR (50)  NOT NULL,
    [CategoryID]     INT           NOT NULL,
    [ImageURL]       VARCHAR (50)  NOT NULL,
    [JobDescription] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Service.Job] PRIMARY KEY CLUSTERED ([JobID] ASC),
    CONSTRAINT [FK_Service.Job_Service.JobCategory] FOREIGN KEY ([CategoryID]) REFERENCES [Service].[JobCategory] ([Id])
);

