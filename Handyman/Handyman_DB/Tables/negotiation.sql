CREATE TABLE [Request].[negotiation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [OrderId] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    [PriceId] INT NOT NULL, 
    [IsCompleted] INT NOT NULL, 
    [Order_DueDate] DATETIME NOT NULL, 
    [LastDateModified] DATETIME NOT NULL
)
