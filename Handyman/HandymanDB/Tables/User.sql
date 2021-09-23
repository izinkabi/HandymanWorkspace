CREATE TABLE [dbo].[User]
(
    [username] VARCHAR(16) NULL, 
    [email] VARCHAR(255) NULL, 
    [password] VARCHAR(32) NULL, 
    [create_time] DATETIME2 (7)  DEFAULT (getutcdate())  NULL, 
    [userId] VARCHAR(45) NOT NULL, 
    PRIMARY KEY ([userId])
)
