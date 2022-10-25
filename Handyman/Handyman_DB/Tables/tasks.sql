CREATE TABLE [Request].[tasks]
(
	[task_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [task_name] NVARCHAR(50) NOT NULL, 
    [task_datecreated] DATETIME2 NOT NULL, 
    [task_status] NCHAR(10) NULL, 
    [task_confirmation] NCHAR(10) NULL, 
    [task_orderid] INT NOT NULL, 
    CONSTRAINT [FK_tasks_order] FOREIGN KEY ([task_orderid]) REFERENCES [Request].[order]([ord_id]) 
)
