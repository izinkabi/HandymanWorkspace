CREATE TABLE [Service].[Category]
(
    [Category_ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Category_Title]    VARCHAR (50)  NOT NULL,
    [Category_Description]    VARCHAR (50)  NOT NULL

     CONSTRAINT [PK_Service.Category] PRIMARY KEY CLUSTERED ([Category_ID] ASC),
   
);