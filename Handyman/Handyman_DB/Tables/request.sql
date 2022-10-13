CREATE TABLE [Request].[request]
(
	[req_id] [int] IDENTITY(1,1) NOT NULL,
	[req_datecreated] [datetime2](7) NOT NULL,
	[req_status] [nchar](10) NOT NULL,
	[req_progress] [nchar](10) NULL, 
    CONSTRAINT [PK_request] PRIMARY KEY ([req_id]),
)
