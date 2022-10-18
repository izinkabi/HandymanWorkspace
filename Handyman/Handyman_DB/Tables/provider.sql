CREATE TABLE [Delivery].[provider]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [pro_employeeid] INT NOT NULL, 
    [pro_serviceid] INT NOT NULL, 
    CONSTRAINT [FK_provider_employees] FOREIGN KEY (pro_employeeid) REFERENCES [Delivery].[employees]([emp_Id]), 
    CONSTRAINT [FK_provider_service] FOREIGN KEY ([pro_serviceid]) REFERENCES [Service].[service](serv_id)
)
