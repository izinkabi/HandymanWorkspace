CREATE TABLE [dbo].[Address]
(
	[AddressId] INT IDENTITY(1,1) NOT NULL, 
    [HouseNumber] INT NOT NULL, 
    [StreetName] VARCHAR(50) NOT NULL, 
    [Surburb] VARCHAR(50) NOT NULL, 
    [PostalCode] INT NOT NULL,
    [City] VARCHAR(100) NOT NULL
    PRIMARY KEY CLUSTERED ([AddressId] ASC)
)
