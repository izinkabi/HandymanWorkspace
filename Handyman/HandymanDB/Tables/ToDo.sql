CREATE TABLE [Service].[ToDo]
(
	[Id] INT NOT NULL, 
    [ItemName] VARCHAR(150) NOT NULL, 
    [Description] VARCHAR(250) NOT NULL, 
    [OrderId] INT NOT NULL, 
    [Type] INT NOT NULL, 
    [StartDate] DATETIME2 NULL, 
    [FinishDate] DATETIME2 NULL, 
    [Status] INT NOT NULL,
    CONSTRAINT [PK_ServiceTodo] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_Service.Todo_Customer.Order] FOREIGN KEY ([OrderId]) REFERENCES [Customer].[Order] ([Id])
)
