CREATE TABLE [Request].[task_price]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [task_Id] INT NOT NULL, 
    [price_Id] INT NOT NULL, 
    CONSTRAINT [FK_task_price_ToPrice] FOREIGN KEY ([price_Id]) REFERENCES [Service].[price]([Id]), 
    CONSTRAINT [FK_task_price_ToTask] FOREIGN KEY ([task_Id]) REFERENCES [Request].[task]([task_id])
)
