CREATE TABLE [Delivery].[business]
(
	[bus_Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [bus_registrationid] INT NOT NULL, 
    [bus_addressid] INT NOT NULL, 
    [bus_datecreated] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_business_registration] FOREIGN KEY ([bus_registrationid]) REFERENCES [Delivery].[registration]([reg_id]), 
    CONSTRAINT [FK_business_address] FOREIGN KEY ([bus_addressid]) REFERENCES [Delivery].[address]([add_id]), 
)
