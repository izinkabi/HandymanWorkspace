--a table for workshop services
CREATE TABLE [Delivery].[WorkShopServices]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [workShopRegId] INT NOT NULL, 
    [customServiceId] INT NOT NULL, 
    CONSTRAINT [FK_WorkShopServices_registration] FOREIGN KEY ([workShopRegId]) REFERENCES [Delivery].[registration]([reg_Id]), 
    CONSTRAINT [FK_WorkShopServices_customService] FOREIGN KEY ([customServiceId]) REFERENCES [Service].[customService]([Id])
)
