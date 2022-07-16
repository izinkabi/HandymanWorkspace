CREATE TABLE [Service].[Service]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(100) NOT NULL, 
    [CategoryID] INT NOT NULL, 
    [ImageUrl] VARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Service.Service] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Service.Service_Service.Category] FOREIGN KEY ([CategoryID]) REFERENCES [Service].[Category]([CategoryId])
)
