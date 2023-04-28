CREATE TABLE [Request].[TaskPrice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [TaskId] INT NOT NULL, 
    [PriceId] INT NOT NULL, 
    CONSTRAINT [FK_TaskPrice_ToPrice] FOREIGN KEY ([PriceId]) REFERENCES [Service].[price]([Id]), 
    CONSTRAINT [FK_TaskPrice_ToTask] FOREIGN KEY ([TaskId]) REFERENCES [Request].[task]([task_id])
)
