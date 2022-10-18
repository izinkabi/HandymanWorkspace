﻿/*
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
USE Handyman_DB
GO

IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('[Service].[category]'))
BEGIN;
    DROP TABLE [Service].[category];
END;
GO

CREATE TABLE [Service].[category] (
    cat_id INT NOT NULL IDENTITY(1, 1),
    [cat_name] VARCHAR(MAX) NULL,
    [cat_description] VARCHAR(MAX) NULL,
    [cat_type] VARCHAR(MAX) NULL,
    PRIMARY KEY (cat_id)
);
GO

INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Electronics','Lorem ipsum dolor','Business-Based'),
  ('Mechanics','Lorem ipsum dolor sit amet, consectetuer adipiscing elit.','In-House'),
  ('Construction','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur','Collection'),
  ('Plumbing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur','Online'),
  ('Cleaning','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','Delivery'),
  ('Electric','Lorem','Business-Based'),
  ('Land Scaping','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','In-House'),
  ('Renovations','Lorem ipsum dolor sit','Collection'),
  ('Roofing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer','Online'),
  ('Maintenance','Lorem ipsum dolor sit amet, consectetuer','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('IT and Support','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur','Business-Based'),
  ('Manual Labou','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam adipiscing','In-House'),
  ('Tiling','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Collection'),
  ('Interior Design','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','Online'),
  ('Exterior Design ','Lorem ipsum dolor sit amet, consectetuer','Delivery'),
  ('Catering','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','Business-Based'),
  ('Events','Lorem ipsum','In-House'),
  ('Electronics','Lorem ipsum','Collection'),
  ('Mechanics','Lorem ipsum dolor','Online'),
  ('Construction','Lorem ipsum','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Plumbing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam adipiscing','Business-Based'),
  ('Cleaning','Lorem ipsum dolor','In-House'),
  ('Electric','Lorem ipsum dolor sit amet, consectetuer adipiscing','Collection'),
  ('Land Scaping','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','Online'),
  ('Renovations','Lorem ipsum dolor sit amet, consectetuer','Delivery'),
  ('Roofing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','Business-Based'),
  ('Maintenance','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer','In-House'),
  ('IT and Support','Lorem ipsum','Collection'),
  ('Manual Labou','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','Online'),
  ('Tiling','Lorem ipsum','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Interior Design','Lorem','Business-Based'),
  ('Exterior Design ','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','In-House'),
  ('Catering','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','Collection'),
  ('Events','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur','Online'),
  ('Electronics','Lorem ipsum dolor sit amet, consectetuer','Delivery'),
  ('Mechanics','Lorem ipsum dolor sit amet, consectetuer','Business-Based'),
  ('Construction','Lorem ipsum dolor','In-House'),
  ('Plumbing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit.','Collection'),
  ('Cleaning','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','Online'),
  ('Electric','Lorem ipsum dolor sit amet,','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Land Scaping','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer','Business-Based'),
  ('Renovations','Lorem ipsum dolor sit amet,','In-House'),
  ('Roofing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit.','Collection'),
  ('Maintenance','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer','Online'),
  ('IT and Support','Lorem ipsum dolor sit','Delivery'),
  ('Manual Labou','Lorem ipsum dolor sit','Business-Based'),
  ('Tiling','Lorem ipsum dolor','In-House'),
  ('Interior Design','Lorem ipsum dolor sit','Collection'),
  ('Exterior Design ','Lorem ipsum dolor','Online'),
  ('Catering','Lorem ipsum','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Events','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','Business-Based'),
  ('Electronics','Lorem ipsum dolor','In-House'),
  ('Mechanics','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Collection'),
  ('Construction','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Online'),
  ('Plumbing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit.','Delivery'),
  ('Cleaning','Lorem ipsum','Business-Based'),
  ('Electric','Lorem ipsum dolor sit amet, consectetuer adipiscing','In-House'),
  ('Land Scaping','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Collection'),
  ('Renovations','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Online'),
  ('Roofing','Lorem','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Maintenance','Lorem ipsum dolor sit amet, consectetuer adipiscing elit.','Business-Based'),
  ('IT and Support','Lorem','In-House'),
  ('Manual Labou','Lorem ipsum dolor sit amet, consectetuer adipiscing','Collection'),
  ('Tiling','Lorem ipsum dolor sit amet,','Online'),
  ('Interior Design','Lorem ipsum dolor sit amet, consectetuer adipiscing elit.','Delivery'),
  ('Exterior Design ','Lorem ipsum dolor sit amet,','Business-Based'),
  ('Catering','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam adipiscing','In-House'),
  ('Events','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam adipiscing','Collection'),
  ('Electronics','Lorem ipsum dolor sit amet, consectetuer','Online'),
  ('Mechanics','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Construction','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur','Business-Based'),
  ('Plumbing','Lorem','In-House'),
  ('Cleaning','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer','Collection'),
  ('Electric','Lorem ipsum dolor sit','Online'),
  ('Land Scaping','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam adipiscing','Delivery'),
  ('Renovations','Lorem ipsum dolor sit','Business-Based'),
  ('Roofing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','In-House'),
  ('Maintenance','Lorem ipsum dolor sit amet, consectetuer','Collection'),
  ('IT and Support','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','Online'),
  ('Manual Labou','Lorem ipsum dolor sit amet, consectetuer','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Tiling','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Business-Based'),
  ('Interior Design','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','In-House'),
  ('Exterior Design ','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer','Collection'),
  ('Catering','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Online'),
  ('Events','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam','Delivery'),
  ('Electronics','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','Business-Based'),
  ('Mechanics','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','In-House'),
  ('Construction','Lorem ipsum dolor sit amet, consectetuer adipiscing','Collection'),
  ('Plumbing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Online'),
  ('Cleaning','Lorem ipsum dolor sit','Delivery');
INSERT INTO [Service].[category] ([cat_name],[cat_description],[cat_type])
VALUES
  ('Electric','Lorem ipsum dolor sit amet,','Business-Based'),
  ('Land Scaping','Lorem ipsum dolor sit amet, consectetuer adipiscing elit.','In-House'),
  ('Renovations','Lorem ipsum dolor','Collection'),
  ('Roofing','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor. Integer','Online'),
  ('Maintenance','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Delivery'),
  ('IT and Support','Lorem ipsum dolor','Business-Based'),
  ('Manual Labou','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed tortor.','In-House'),
  ('Tiling','Lorem ipsum','Collection'),
  ('Interior Design','Lorem ipsum dolor sit amet, consectetuer','Online'),
  ('Exterior Design ','Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Curabitur sed','Delivery');