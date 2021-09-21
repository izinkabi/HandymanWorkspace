CREATE TABLE [dbo].[User]
(
    [username] VARCHAR(16) NULL, 
    [email] VARCHAR(255) NULL, 
    [password] VARCHAR(32) NULL, 
    [create_time] TIMESTAMP NULL, 
    [userId] VARCHAR(45) NOT NULL, 
    PRIMARY KEY ([userId])
)
