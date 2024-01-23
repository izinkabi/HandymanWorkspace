CREATE TABLE [Request].[negotiation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [neg_requestId] INT NOT NULL, 
    [neg_PriceId] INT NOT NULL, 
    [neg_IsCompleted] INT NOT NULL, 
    [neg_Order_DueDate] DATETIME NOT NULL, 
    [neg_LastDateModified] DATETIME NOT NULL, 
    [neg_DateCreated] NCHAR(10) NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [FK_negotiation_request] FOREIGN KEY ([neg_requestId]) REFERENCES [Delivery].[order]([order_id])
)
