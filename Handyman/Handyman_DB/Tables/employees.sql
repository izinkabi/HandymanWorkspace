CREATE TABLE [Delivery].[employees]
(
    [emp_businessid] INT NOT NULL, 
    [emp_userid] NVARCHAR(450) UNIQUE NOT NULL , 
    [emp_date_employed] DATETIME2 NOT NULL, 
    [emp_profile_id] NVARCHAR(450) NOT NULL, 
    --CONSTRAINT [FK_employees_business] FOREIGN KEY ([emp_businessid]) REFERENCES [Delivery].[business]([bus_Id]), 
    CONSTRAINT [PK_employees_emp_userid] PRIMARY KEY ([emp_userid]), 
   -- CONSTRAINT [FK_employees_AspNetUsers] FOREIGN KEY ([emp_userid]) REFERENCES [dbo].[AspNetUsers]([Id])
 
    --CONSTRAINT [FK_employees_Profile] FOREIGN KEY ([emp_profile_id]) REFERENCES [dbo].[Profile]([UserId])    
)
