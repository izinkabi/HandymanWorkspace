CREATE TABLE [dbo].[Service]
(
	[ServiceId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(45) NOT NULL,
    [ServiceCategoryId] int NOT NULL,
    [Description] VARCHAR(100) NOT NULL,
    CONSTRAINT [FK_Service_ServiceCategory] FOREIGN KEY ([ServiceCategoryId]) REFERENCES [ServiceCategory]([Id])
)

