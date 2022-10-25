CREATE TABLE [Request].[order]
(
	[ord_id] [int] IDENTITY(1,1) NOT NULL,
	[ord_datecreated] [datetime2](7) NOT NULL,
	[ord_status] [nchar](10) NOT NULL,
	[ord_duedate] [datetime2](7) NOT NULL, 
    [ord_userid] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_order] PRIMARY KEY ([ord_id]),
)
