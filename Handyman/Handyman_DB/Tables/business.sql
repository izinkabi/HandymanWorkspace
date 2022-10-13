CREATE TABLE [Delivery].[business]
(
	[bus_Id] INT NOT NULL PRIMARY KEY, 
    [bus_employeeid] INT NOT NULL, 
    [bus_registrationid] INT NOT NULL, 
    [bus_addressid] INT NOT NULL, 
    [bus_datecreated] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_business_employees] FOREIGN KEY ([bus_employeeid]) REFERENCES [Delivery].[employees]([emp_id]), 
    CONSTRAINT [FK_business_registration] FOREIGN KEY ([bus_registrationid]) REFERENCES [Delivery].[registation]([reg_id]), 
    CONSTRAINT [FK_business_address] FOREIGN KEY ([bus_addressid]) REFERENCES [Delivery].[address]([add_id]), 
)
