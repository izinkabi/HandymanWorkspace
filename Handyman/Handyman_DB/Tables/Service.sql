CREATE TABLE [Service].[service]
(
	[serv_id] [int] IDENTITY(1,1) NOT NULL,
	[serv_name] [nchar](30) NOT NULL,
	[serv_img] [varchar](max) NOT NULL,
	[serv_categoryid] [int] NOT NULL,
	[serv_datecreated] [datetime] NOT NULL,
	[serv_status] [nchar](20) NOT NULL,
)
