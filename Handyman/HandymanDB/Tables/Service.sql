CREATE TABLE [dbo].[Service]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(45) NOT NULL,
    [ServiceCategoryId] int NOT NULL,
    [Description] VARCHAR(100) NOT NULL,
    [ImageUrl] NVARCHAR(100) NOT NULL, 
    CONSTRAINT [FK_Service_ServiceCategory] FOREIGN KEY ([ServiceCategoryId]) REFERENCES [ServiceCategory]([Id])
)

