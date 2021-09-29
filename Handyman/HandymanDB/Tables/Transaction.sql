CREATE TABLE [dbo].[Transaction]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Date] DATETIME NULL, 
    [ServiceId] INT NULL, 
    [Type] VARCHAR(45) NULL, 
    [ConsumerId] INT NULL, 
    [RequestId] INT NULL, 
    CONSTRAINT [FK_Transaction_Consumer] FOREIGN KEY ([ConsumerId]) REFERENCES [Consumer]([ConsumerId])
)
