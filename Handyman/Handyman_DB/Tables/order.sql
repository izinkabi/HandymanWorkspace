CREATE TABLE [Delivery].[order]
(
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[order_datecreated] [datetime2](7) NOT NULL,
	[order_status] INT NOT NULL,
	[order_progress] INT NULL, 
    [order_employeeid] NVARCHAR (450) NOT NULL, 
    [order_requestId] INT NOT NULL, 
    CONSTRAINT [PK_order] PRIMARY KEY ([order_id]), 
    CONSTRAINT [FK_order_employees] FOREIGN KEY ([order_employeeId]) REFERENCES [Delivery].[employee]([emp_profile_id]), 
    CONSTRAINT [FK_order_request] FOREIGN KEY (order_requestId) REFERENCES [Request].[request]([req_id]),

)
