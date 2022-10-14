CREATE TABLE [Delivery].[employees]
(
	[emp_Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [emp_userid] NVARCHAR(MAX) NOT NULL, 
    [emp_businessid] INT NOT NULL, 
    [emp_ratingid] INT NOT NULL, 
    CONSTRAINT [FK_employees_business] FOREIGN KEY ([emp_businessid]) REFERENCES [Delivery].[business]([bus_Id]), 
    CONSTRAINT [FK_employees_ratings] FOREIGN KEY (emp_ratingid) REFERENCES [Delivery].[ratings](rate_id) 
)
