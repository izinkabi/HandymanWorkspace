CREATE TABLE [Customer].[ToDo]
(
	[Id] INT IDENTITY(1,1) NOT NULL , 
    [ItemName] VARCHAR(150) NOT NULL, 
    [Description] VARCHAR(250) NOT NULL, 
    [OrderId] INT NOT NULL, 
    [Type] INT NOT NULL, 
    [StartDate] DATETIME2 NULL, 
    [FinishDate] DATETIME2 NULL, 
    [Status] INT NOT NULL,
    CONSTRAINT [PK_CustomerTodo] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_Customer.Todo_Customer.Order] FOREIGN KEY ([OrderId]) REFERENCES [Customer].[Order] ([Id])
)
