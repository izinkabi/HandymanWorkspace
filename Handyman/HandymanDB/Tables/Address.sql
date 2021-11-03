CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [House Number] NVARCHAR(50) NOT NULL, 
    [Street Name] VARCHAR(50) NOT NULL, 
    [Surburb] VARCHAR(50) NOT NULL, 
    [Postal Code] INT NOT NULL
)
