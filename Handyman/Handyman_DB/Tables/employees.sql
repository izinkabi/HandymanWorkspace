CREATE TABLE [Delivery].[employees]
(
    [emp_businessid] INT NOT NULL, 
    [emp_ratingid] INT NOT NULL, 
    [emp_userid] NVARCHAR(450) NOT NULL , 
    CONSTRAINT [FK_employees_business] FOREIGN KEY ([emp_businessid]) REFERENCES [Delivery].[business]([bus_Id]), 
    CONSTRAINT [FK_employees_ratings] FOREIGN KEY (emp_ratingid) REFERENCES [Delivery].[ratings](rate_id), 
    CONSTRAINT [PK_employees_emp_userid] PRIMARY KEY ([emp_userid]), 
   -- CONSTRAINT [FK_employees_AspNetUsers] FOREIGN KEY ([emp_userid]) REFERENCES [dbo].[AspNetUsers]([Id])
    
)
