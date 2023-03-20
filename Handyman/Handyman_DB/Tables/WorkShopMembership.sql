CREATE TABLE [dbo].[WorkShopMembership]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ProfileId] NVARCHAR(450) NOT NULL, 
    [BusinessId] INT NOT NULL, 
    [CreationDate] DATETIME2 NOT NULL, 
    [IsMemberAccepted] INT NULL DEFAULT 0, 
    [IsWorkShopApproved] INT NULL DEFAULT 0, 
    [ExpiryDate] DATETIME2 NULL, 
    CONSTRAINT [FK_WorkShopMembership_ToProfile] FOREIGN KEY ([ProfileId]) REFERENCES [dbo].[profile]([UserId])
)
