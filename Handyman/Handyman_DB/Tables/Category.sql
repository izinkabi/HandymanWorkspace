CREATE TABLE [Service].[category]
(
    [cat_id] [int] IDENTITY(1,1) NOT NULL,
	[cat_name] [nchar](50) NOT NULL,
	[cat_description] [nvarchar](max) NOT NULL,
	[cat_type] [nchar](50) NOT NULL, 
    CONSTRAINT [PK_category] PRIMARY KEY ([cat_id]),
)
