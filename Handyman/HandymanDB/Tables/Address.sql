CREATE TABLE [dbo].[Address]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, 
    [House Number] INT NOT NULL, 
    [Street Name] VARCHAR(50) NOT NULL, 
    [Surburb] VARCHAR(50) NOT NULL, 
    [Postal Code] INT NOT NULL,
    [City] VARCHAR(100) NOT NULL
)
