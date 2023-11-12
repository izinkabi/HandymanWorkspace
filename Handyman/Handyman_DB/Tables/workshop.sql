CREATE TABLE [Delivery].[workshop]
(
	[work_Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [work_registrationid] INT NOT NULL, 
    [work_addressid] INT NOT NULL, 
    [work_datecreated] DATETIME2 NOT NULL, 

    [work_name] VARCHAR(150) NOT NULL, 
    [work_description] NVARCHAR(400) NOT NULL, 
    CONSTRAINT [FK_workshop_registration] FOREIGN KEY ([work_registrationid]) REFERENCES [Delivery].[registration]([reg_id]), 
    CONSTRAINT [FK_workshop_address] FOREIGN KEY ([work_addressid]) REFERENCES [Delivery].[address]([add_id]), 
)
