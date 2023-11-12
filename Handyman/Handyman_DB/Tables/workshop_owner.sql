CREATE TABLE [Delivery].[workshop_owner]
(
	[owner_Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [owner_member_id] nvarchar(450) NOT NULL, 
    CONSTRAINT [FK_workshop_owner_employee] FOREIGN KEY ([owner_member_id]) REFERENCES [Delivery].[member]([member_profileId]),
   
)
