CREATE TABLE [Delivery].[employee]
(
    [emp_workshopid] INT NOT NULL, 
    [emp_date_employed] DATETIME2 NOT NULL, 
    [emp_profile_id] NVARCHAR(450) NOT NULL, 
    [emp_role] INT NOT NULL, 
    CONSTRAINT [FK_employee_workshop] FOREIGN KEY ([emp_workshopid]) REFERENCES [Delivery].[workshop]([work_Id]), 
    CONSTRAINT [PK_employee_emp_profile_id] PRIMARY KEY ([emp_profile_id]),
    CONSTRAINT [FK_employee_AspNetUsers] FOREIGN KEY ([emp_profile_id]) REFERENCES [dbo].[profile]([UserId])
 
       
)
