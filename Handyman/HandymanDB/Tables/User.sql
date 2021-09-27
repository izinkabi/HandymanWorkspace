CREATE TABLE [dbo].[User]
(
    [username] NVARCHAR(255) NOT NULL, 
    [email] VARCHAR(255) NOT NULL, 
    [password] VARCHAR(32)  NULL, 
    [create_time] DATETIME2 (7)  DEFAULT (getutcdate())  NULL, 
    [Id] NVARCHAR(128) NOT NULL, 
    PRIMARY KEY ([Id])
)
