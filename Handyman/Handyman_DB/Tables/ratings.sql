CREATE TABLE [Delivery].[ratings]
(
	[rate_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [rate_stars] INT NOT NULL, 
    [rate_review] NVARCHAR(MAX) NULL, 
    [rate_recommendation] NCHAR(10) NULL
)
