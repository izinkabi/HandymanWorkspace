CREATE TABLE [Delivery].[request]
(
	[req_id] [int] IDENTITY(1,1) NOT NULL,
	[req_datecreated] [datetime2](7) NOT NULL,
	[req_status] INT NOT NULL,
	[req_progress] INT NULL, 
    [req_employeeid] NVARCHAR (450) NOT NULL, 
    [req_orderid] INT NOT NULL, 
    CONSTRAINT [PK_request] PRIMARY KEY ([req_id]), 
    CONSTRAINT [FK_request_employees] FOREIGN KEY ([req_employeeid]) REFERENCES [Delivery].[employees]([emp_profile_id]), 
    CONSTRAINT [FK_request_order] FOREIGN KEY (req_orderid) REFERENCES [Request].[order]([ord_id]),
)
