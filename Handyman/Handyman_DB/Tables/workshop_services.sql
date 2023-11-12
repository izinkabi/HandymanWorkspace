--a table for workshop services
CREATE TABLE [Delivery].[workshop_services]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [work_RegId] INT NOT NULL, 
    [customServiceId] INT NOT NULL, 
    CONSTRAINT [FK_workshopservices_registration] FOREIGN KEY ([work_RegId]) REFERENCES [Delivery].[registration]([reg_Id]), 
    CONSTRAINT [FK_workshopservices_customService] FOREIGN KEY ([customServiceId]) REFERENCES [Service].[customService]([Id])
)
