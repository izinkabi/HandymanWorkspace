CREATE TABLE [dbo].[User]
(
    [username] VARCHAR(255) NOT NULL, 
    [email] VARCHAR(255) NOT NULL, 
    [password] VARCHAR(32) NOT NULL, 
    [create_time] DATETIME2 (7)  DEFAULT (getutcdate())  NULL, 
    [userId] VARCHAR(45) NOT NULL, 
    PRIMARY KEY ([userId])
)
