CREATE TABLE [Delivery].[employee_service]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 20), 
    [emp_id]  NVARCHAR (450) NOT NULL, 
    [serv_id] INT NOT NULL, 
    CONSTRAINT [FK_employee_service_employees] FOREIGN KEY ([emp_id]) REFERENCES [Delivery].[employees]([emp_userid]),
    CONSTRAINT [FK_employee_service_services] FOREIGN KEY ([serv_id]) REFERENCES [Service].[service]([serv_id])
)
