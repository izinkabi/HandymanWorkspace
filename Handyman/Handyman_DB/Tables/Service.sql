CREATE TABLE [Service].[service]
(
	[serv_id] [int] IDENTITY(1,1) NOT NULL,
	[serv_name] [nchar](30) NOT NULL,
	[serv_img] [varchar](max) NOT NULL,
	[serv_categoryid] [int] NOT NULL,
	[serv_datecreated] [datetime] NOT NULL,
	[serv_status] [nchar](20) NOT NULL, 
    [price_id] INT NOT NULL, 
    CONSTRAINT [FK_service_category] FOREIGN KEY ([serv_categoryid]) REFERENCES [Service].[category]([cat_id]), 
    CONSTRAINT [PK_service] PRIMARY KEY ([serv_id]), 
    CONSTRAINT [FK_service_price] FOREIGN KEY ([price_id]) REFERENCES [Service].[price]([Id]) 
)
