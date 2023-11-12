CREATE TABLE [Request].[request_task]
(
	[Id] INT IDENTITY(1,1) NOT NULL  PRIMARY KEY, 
    [request_id] INT NOT NULL, 
    [task_id] INT NOT NULL, 
    CONSTRAINT [FK_order_task_order] FOREIGN KEY ([request_id]) REFERENCES [Request].[request]([req_id]), 
    CONSTRAINT [FK_order_task_task] FOREIGN KEY ([task_id]) REFERENCES [Request].[task]([task_id])
)
