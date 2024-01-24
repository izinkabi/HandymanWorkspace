/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/*script generated for free with OnlineDataGenerator available at: https://www.onlinedatagenerator.com */
Use [Handyman_DB]
Go

INSERT INTO [Service].[Category] VALUES('Clerk','Work Under Pressure','Purchasing');
INSERT INTO [Service].[Category] VALUES('Steward','Self-motivation','Human Resources');
INSERT INTO [Service].[Category] VALUES('Cashier','Teamwork','Sales');
INSERT INTO [Service].[Category] VALUES('Executive Director','Teamwork','Human Resources');
INSERT INTO [Service].[Category] VALUES('Laboratory Technician','Conflict Resolution','IT');
INSERT INTO [Service].[Category] VALUES('Mobile Developer','Decision Making','Human Resources');
INSERT INTO [Service].[Category] VALUES('Global Logistics Supervisor','Adaptability','IT');
INSERT INTO [Service].[Category] VALUES('Stockbroker','Communication','Finance');
INSERT INTO [Service].[Category] VALUES('Software Engineer','Conflict Resolution','Sales');
INSERT INTO [Service].[Category] VALUES('Physician','Adaptability','Purchasing');
INSERT INTO [Service].[Category] VALUES('Staffing Consultant','Teamwork','Research and Development');
INSERT INTO [Service].[Category] VALUES('Production Painter','Time Management','Marketing');
INSERT INTO [Service].[Category] VALUES('Bellman','Communication','Research and Development');
INSERT INTO [Service].[Category] VALUES('Bellman','Learning','Human Resources');
INSERT INTO [Service].[Category] VALUES('Stockbroker','Leadership','Research and Development');
INSERT INTO [Service].[Category] VALUES('Electrician','Leadership','Management');
INSERT INTO [Service].[Category] VALUES('Front Desk Coordinator','Teamwork','Accounting');
INSERT INTO [Service].[Category] VALUES('Steward','Learning','Sales');
INSERT INTO [Service].[Category] VALUES('Stockbroker','Time Management','Operations');
INSERT INTO [Service].[Category] VALUES('Steward','Decision Making','Finance');
INSERT INTO [Service].[Category] VALUES('Webmaster','Work Under Pressure','Research and Development');
INSERT INTO [Service].[Category] VALUES('Global Logistics Supervisor','Learning','Purchasing');
INSERT INTO [Service].[Category] VALUES('Health Educator','Self-motivation','Research and Development');
INSERT INTO [Service].[Category] VALUES('Physician','Self-motivation','Marketing');
INSERT INTO [Service].[Category] VALUES('Baker','Time Management','Research and Development');
INSERT INTO [Service].[Category] VALUES('IT Support Staff','Adaptability','Marketing');
INSERT INTO [Service].[Category] VALUES('Dentist','Teamwork','Accounting');
INSERT INTO [Service].[Category] VALUES('Audiologist','Leadership','Purchasing');
INSERT INTO [Service].[Category] VALUES('Biologist','Communication','IT');
INSERT INTO [Service].[Category] VALUES('Pharmacist','Time Management','Purchasing');
INSERT INTO [Service].[Category] VALUES('Executive Director','Teamwork','Research and Development');
INSERT INTO [Service].[Category] VALUES('Loan Officer','Self-motivation','Accounting');
INSERT INTO [Service].[Category] VALUES('Front Desk Coordinator','Adaptability','Finance');
INSERT INTO [Service].[Category] VALUES('Insurance Broker','Learning','IT');
INSERT INTO [Service].[Category] VALUES('Webmaster','Work Under Pressure','Accounting');
INSERT INTO [Service].[Category] VALUES('Associate Professor','Time Management','Operations');
INSERT INTO [Service].[Category] VALUES('Inspector','Communication','Sales');
INSERT INTO [Service].[Category] VALUES('Ambulatory Nurse','Adaptability','Accounting');
INSERT INTO [Service].[Category] VALUES('Audiologist','Adaptability','Sales');
INSERT INTO [Service].[Category] VALUES('Treasurer','Leadership','Research and Development');
INSERT INTO [Service].[Category] VALUES('Audiologist','Communication','Sales');
INSERT INTO [Service].[Category] VALUES('Bookkeeper','Time Management','Operations');
INSERT INTO [Service].[Category] VALUES('Cashier','Decision Making','Human Resources');
INSERT INTO [Service].[Category] VALUES('Ambulatory Nurse','Leadership','Finance');
INSERT INTO [Service].[Category] VALUES('Fabricator','Communication','IT');
INSERT INTO [Service].[Category] VALUES('Operator','Teamwork','Human Resources');
INSERT INTO [Service].[Category] VALUES('Production Painter','Communication','IT');
INSERT INTO [Service].[Category] VALUES('Dentist','Adaptability','Accounting');
INSERT INTO [Service].[Category] VALUES('Cashier','Communication','Finance');
INSERT INTO [Service].[Category] VALUES('Ambulatory Nurse','Decision Making','Accounting');

