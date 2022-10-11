CREATE TABLE [Service].[rating](
	[rate_id] [int] IDENTITY(1,1) NOT NULL,
	[rate_comment] [nvarchar](max) NOT NULL,
	[rate_datecreated] [datetime2](7) NOT NULL,
	[rate_scale] [int] NULL
    )