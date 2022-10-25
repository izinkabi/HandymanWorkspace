CREATE TABLE [Request].[order_task]
(
	[Id] INT IDENTITY(1,20) NOT NULL  PRIMARY KEY, 
    [order_id] INT NOT NULL, 
    [task_id] INT NOT NULL, 
    CONSTRAINT [FK_order_task_order] FOREIGN KEY ([order_id]) REFERENCES [Request].[order]([ord_id]), 
    CONSTRAINT [FK_order_task_task] FOREIGN KEY ([task_id]) REFERENCES [Request].[task]([task_id])
)
