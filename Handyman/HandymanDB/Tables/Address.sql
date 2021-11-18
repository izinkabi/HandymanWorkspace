CREATE TABLE [dbo].[Address]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, 
    [HouseNumber] INT NOT NULL, 
    [StreetName] VARCHAR(50) NOT NULL, 
    [Surburb] VARCHAR(50) NOT NULL, 
    [PostalCode] INT NOT NULL,
    [City] VARCHAR(100) NOT NULL
)
