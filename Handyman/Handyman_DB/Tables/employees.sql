CREATE TABLE [Delivery].[employees]
(
    [emp_businessid] INT NOT NULL, 
    [emp_date_employed] DATETIME2 NOT NULL, 
    [emp_profile_id] NVARCHAR(450) NOT NULL, 
    [emp_role] INT NOT NULL, 
    CONSTRAINT [FK_employees_business] FOREIGN KEY ([emp_businessid]) REFERENCES [Delivery].[business]([bus_Id]), 
    CONSTRAINT [PK_employees_emp_userid] PRIMARY KEY ([emp_profile_id]),
    CONSTRAINT [FK_employees_AspNetUsers] FOREIGN KEY ([emp_profile_id]) REFERENCES [dbo].[profile]([UserId])
 
       
)
