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
USE [Handyman_DB]
GO

/****** Object:  Table [Service].[service]    Script Date: 2022/10/11 17:46:05 ******/
--IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('[Service].[service]'))
--BEGIN;
--    DROP TABLE [Service].[service];
--END;
--GO

--CREATE TABLE [Service].[service] (
--    [serv_id] [int] NOT NULL IDENTITY(1, 1),
--    [serv_name] VARCHAR(MAX) NULL,
--    [serv_img] VARCHAR(255) NULL,
--    [serv_categoryid] INTEGER NULL,
--    [serv_datecreated] VARCHAR(255) NULL,
--    [serv_status] VARCHAR(MAX) NULL,
--    PRIMARY KEY ([serv_id])
--);
--GO

INSERT INTO [Service].[service] (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('House Cleaning','https://handyman/group/9',16,'2022-06-11 22:10:40','Available'),
  ('Walls painting','http://handyman/fr',34,'2022-11-01 14:02:45','Available'),
  ('Welding','http://handyman/user/110',24,'2023-07-22 00:52:32','Unavailable'),
  ('Swimming pool','https://handyman/fr',14,'2023-01-08 19:41:37','Unavailable'),
  ('Grass cutting','http://handyman/user/110',22,'2021-10-08 06:55:04','Out-of-service'),
  ('Bush trimming','https://handyman/group/9',6,'2021-10-30 23:04:14','Out-of-service'),
  ('Paiving','http://handyman/en-ca',13,'2023-07-11 04:39:54','Available'),
  ('Gyser Installation','http://handyman/one',0,'2022-10-17 06:28:30','Available'),
  ('Phone repairs','https://handyman/en-us',26,'2021-10-14 17:56:10','Unavailable'),
  ('Computer repairs','https://handyman/sub',30,'2022-05-20 18:54:59','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Lights installation','https://handyman/en-us',39,'2021-10-29 14:17:10','Out-of-service'),
  ('Car repairs','http://handyman/user/110',18,'2022-05-16 19:05:32','Out-of-service'),
  ('Tacth Building','http://handyman/settings',9,'2022-02-21 00:41:46','Available'),
  ('Sink repairs','http://handyman/group/9',11,'2022-06-08 17:25:58','Available'),
  ('Stove repairs','http://handyman/settings',9,'2023-04-21 17:52:26','Unavailable'),
  ('Microwave repairs','http://handyman/site',13,'2022-11-27 15:56:22','Unavailable'),
  ('Fridge repairs','https://handyman/sub/cars',25,'2022-05-31 07:04:31','Out-of-service'),
  ('Ipad repairs','https://handyman/en-us',13,'2023-01-23 23:29:03','Out-of-service'),
  ('Tile laying','https://handyman/en-ca',9,'2022-02-07 21:14:56','Available'),
  ('Demolition','https://handyman/fr',12,'2023-04-11 18:18:08','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Renovation','http://handyman/user/110',1,'2022-11-19 02:17:30','Unavailable'),
  ('House Cleaning','http://handyman/fr',38,'2023-03-22 07:01:00','Unavailable'),
  ('Walls painting','http://handyman/one',29,'2023-06-10 10:14:49','Out-of-service'),
  ('Welding','http://handyman/fr',20,'2022-11-21 11:12:20','Out-of-service'),
  ('Swimming pool','http://handyman/site',24,'2023-03-18 15:45:27','Available'),
  ('Grass cutting','https://handyman/site',2,'2022-02-26 19:26:22','Available'),
  ('Bush trimming','http://handyman/one',2,'2021-12-12 03:05:40','Unavailable'),
  ('Paiving','http://handyman/sub/cars',34,'2022-05-15 20:57:23','Unavailable'),
  ('Gyser Installation','http://handyman/en-ca',13,'2022-10-30 22:38:04','Out-of-service'),
  ('Phone repairs','https://handyman/fr',29,'2021-10-27 17:58:43','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Computer repairs','https://handyman/site',18,'2022-03-03 21:47:01','Available'),
  ('Lights installation','http://handyman/en-ca',15,'2022-06-26 18:10:04','Available'),
  ('Car repairs','https://handyman/en-us',7,'2022-01-07 07:39:54','Unavailable'),
  ('Tacth Building','https://handyman/group/9',6,'2023-01-25 23:20:46','Unavailable'),
  ('Sink repairs','https://handyman/en-ca',26,'2022-02-01 18:23:40','Out-of-service'),
  ('Stove repairs','https://handyman/en-us',32,'2023-01-09 18:29:04','Out-of-service'),
  ('Microwave repairs','https://handyman/fr',9,'2022-08-15 04:09:53','Available'),
  ('Fridge repairs','https://handyman/one',32,'2022-01-06 09:03:06','Available'),
  ('Ipad repairs','http://handyman/en-ca',18,'2021-10-04 12:36:55','Unavailable'),
  ('Tile laying','https://handyman/en-ca',2,'2022-01-05 14:21:34','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Demolition','http://handyman/sub/cars',18,'2022-04-07 18:03:14','Out-of-service'),
  ('Renovation','http://handyman/sub/cars',32,'2022-09-03 17:19:25','Out-of-service'),
  ('House Cleaning','https://handyman/en-ca',16,'2022-08-28 16:15:58','Available'),
  ('Walls painting','http://handyman/sub',24,'2021-12-10 18:03:25','Available'),
  ('Welding','http://handyman/user/110',34,'2023-03-04 07:27:37','Unavailable'),
  ('Swimming pool','http://handyman/settings',23,'2022-05-31 04:10:05','Unavailable'),
  ('Grass cutting','http://handyman/settings',5,'2022-04-22 13:34:23','Out-of-service'),
  ('Bush trimming','http://handyman/en-ca',2,'2022-07-20 13:55:42','Out-of-service'),
  ('Paiving','https://handyman/settings',19,'2023-04-09 22:46:26','Available'),
  ('Gyser Installation','https://handyman/one',5,'2022-05-18 19:19:52','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Phone repairs','https://handyman/en-us',37,'2022-05-19 00:52:10','Unavailable'),
  ('Computer repairs','http://handyman/group/9',39,'2023-05-03 10:52:10','Unavailable'),
  ('Lights installation','https://handyman/sub',32,'2023-07-14 22:01:20','Out-of-service'),
  ('Car repairs','http://handyman/group/9',10,'2022-12-26 14:56:10','Out-of-service'),
  ('Tacth Building','https://handyman/sub',21,'2023-04-02 02:20:12','Available'),
  ('Sink repairs','https://handyman/site',5,'2022-05-27 17:11:05','Available'),
  ('Stove repairs','https://handyman/en-us',11,'2023-06-05 22:21:18','Unavailable'),
  ('Microwave repairs','https://handyman/fr',34,'2022-09-27 02:40:06','Unavailable'),
  ('Fridge repairs','http://handyman/one',16,'2022-11-12 03:38:52','Out-of-service'),
  ('Ipad repairs','https://handyman/sub/cars',38,'2022-01-14 19:46:50','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Tile laying','https://handyman/group/9',32,'2023-07-02 11:20:29','Available'),
  ('Demolition','http://handyman/site',27,'2022-08-21 07:31:09','Available'),
  ('Renovation','http://handyman/en-ca',37,'2022-06-05 09:29:13','Unavailable'),
  ('House Cleaning','http://handyman/user/110',36,'2022-05-15 14:03:06','Unavailable'),
  ('Walls painting','https://handyman/en-us',24,'2022-09-19 23:34:53','Out-of-service'),
  ('Welding','http://handyman/sub',21,'2023-06-29 02:36:21','Out-of-service'),
  ('Swimming pool','https://handyman/sub',15,'2021-12-25 09:56:19','Available'),
  ('Grass cutting','http://handyman/site',13,'2022-01-05 01:21:58','Available'),
  ('Bush trimming','http://handyman/fr',29,'2023-03-26 07:12:37','Unavailable'),
  ('Paiving','https://handyman/sub',26,'2023-04-29 17:40:15','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Gyser Installation','http://handyman/en-us',18,'2022-01-09 21:40:27','Out-of-service'),
  ('Phone repairs','http://handyman/user/110',35,'2022-09-02 19:34:06','Out-of-service'),
  ('Computer repairs','https://handyman/en-ca',29,'2023-06-12 12:39:34','Available'),
  ('Lights installation','https://handyman/en-ca',36,'2021-12-12 01:17:25','Available'),
  ('Car repairs','http://handyman/site',10,'2023-03-10 11:48:17','Unavailable'),
  ('Tacth Building','http://handyman/site',37,'2023-08-23 16:12:30','Unavailable'),
  ('Sink repairs','http://handyman/group/9',11,'2023-02-17 05:44:34','Out-of-service'),
  ('Stove repairs','http://handyman/site',8,'2022-06-10 15:33:07','Out-of-service'),
  ('Microwave repairs','https://handyman/en-us',1,'2023-01-20 16:25:12','Available'),
  ('Fridge repairs','http://handyman/group/9',25,'2022-04-23 21:01:06','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Ipad repairs','http://handyman/sub',14,'2022-10-13 09:55:00','Unavailable'),
  ('Tile laying','http://handyman/settings',38,'2023-04-08 23:20:57','Unavailable'),
  ('Demolition','http://handyman/site',32,'2023-07-30 01:39:05','Out-of-service'),
  ('Renovation','https://handyman/fr',8,'2023-01-20 00:21:42','Out-of-service'),
  ('House Cleaning','https://handyman/one',1,'2022-07-30 01:08:57','Available'),
  ('Walls painting','http://handyman/en-ca',26,'2022-04-26 09:50:04','Available'),
  ('Welding','https://handyman/en-ca',26,'2022-02-24 11:56:11','Unavailable'),
  ('Swimming pool','https://handyman/one',0,'2022-07-16 13:07:02','Unavailable'),
  ('Grass cutting','https://handyman/user/110',31,'2022-04-16 03:48:47','Out-of-service'),
  ('Bush trimming','https://handyman/one',1,'2021-11-22 02:38:48','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Paiving','https://handyman/en-us',34,'2022-06-09 21:07:43','Available'),
  ('Gyser Installation','http://handyman/group/9',0,'2022-09-02 14:01:06','Available'),
  ('Phone repairs','https://handyman/en-us',33,'2023-01-16 01:29:59','Unavailable'),
  ('Computer repairs','https://handyman/group/9',5,'2022-12-31 11:14:32','Unavailable'),
  ('Lights installation','https://handyman/group/9',21,'2022-01-22 00:20:57','Out-of-service'),
  ('Car repairs','https://handyman/en-us',19,'2021-10-31 22:01:56','Out-of-service'),
  ('Tacth Building','http://handyman/group/9',16,'2023-04-24 18:17:26','Available'),
  ('Sink repairs','https://handyman/user/110',17,'2022-12-17 01:22:59','Available'),
  ('Stove repairs','http://handyman/sub/cars',9,'2022-02-26 17:59:27','Unavailable'),
  ('Microwave repairs','http://handyman/sub',11,'2022-08-28 17:32:06','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Fridge repairs','https://handyman/settings',21,'2023-08-07 08:09:42','Out-of-service'),
  ('Ipad repairs','https://handyman/en-ca',15,'2021-11-05 19:09:43','Out-of-service'),
  ('Tile laying','http://handyman/fr',25,'2023-08-16 09:11:00','Available'),
  ('Demolition','http://handyman/group/9',20,'2022-12-25 09:30:44','Available'),
  ('Renovation','http://handyman/settings',4,'2023-09-10 22:38:03','Unavailable'),
  ('House Cleaning','http://handyman/site',28,'2023-02-15 15:54:46','Unavailable'),
  ('Walls painting','https://handyman/en-ca',14,'2023-03-27 03:49:00','Out-of-service'),
  ('Welding','https://handyman/site',27,'2023-07-15 04:50:11','Out-of-service'),
  ('Swimming pool','http://handyman/en-ca',17,'2022-03-11 02:24:13','Available'),
  ('Grass cutting','http://handyman/settings',23,'2022-12-10 11:11:47','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Bush trimming','https://handyman/en-ca',38,'2023-01-07 12:26:13','Unavailable'),
  ('Paiving','http://handyman/fr',24,'2022-07-26 08:34:04','Unavailable'),
  ('Gyser Installation','http://handyman/one',1,'2021-11-01 10:03:22','Out-of-service'),
  ('Phone repairs','https://handyman/group/9',30,'2022-09-04 19:34:14','Out-of-service'),
  ('Computer repairs','https://handyman/en-us',16,'2022-04-18 15:54:26','Available'),
  ('Lights installation','https://handyman/site',5,'2022-07-28 21:21:41','Available'),
  ('Car repairs','http://handyman/group/9',30,'2023-06-20 01:07:16','Unavailable'),
  ('Tacth Building','http://handyman/user/110',1,'2022-05-30 11:36:43','Unavailable'),
  ('Sink repairs','http://handyman/fr',1,'2023-02-24 20:56:18','Out-of-service'),
  ('Stove repairs','http://handyman/settings',21,'2022-01-25 08:04:14','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Microwave repairs','https://handyman/en-ca',16,'2021-12-26 15:30:06','Available'),
  ('Fridge repairs','http://handyman/group/9',35,'2022-07-03 07:20:36','Available'),
  ('Ipad repairs','https://handyman/fr',4,'2023-03-25 14:36:11','Unavailable'),
  ('Tile laying','http://handyman/settings',2,'2023-04-19 02:34:39','Unavailable'),
  ('Demolition','https://handyman/settings',17,'2022-02-26 20:31:26','Out-of-service'),
  ('Renovation','http://handyman/group/9',38,'2023-06-28 22:18:35','Out-of-service'),
  ('House Cleaning','http://handyman/one',2,'2022-06-20 03:48:00','Available'),
  ('Walls painting','http://handyman/site',27,'2023-05-15 01:45:15','Available'),
  ('Welding','http://handyman/en-ca',10,'2022-12-03 23:52:15','Unavailable'),
  ('Swimming pool','http://handyman/one',21,'2023-05-15 13:10:37','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Grass cutting','http://handyman/user/110',30,'2023-04-11 16:04:43','Out-of-service'),
  ('Bush trimming','http://handyman/sub',14,'2022-10-27 02:44:24','Out-of-service'),
  ('Paiving','http://handyman/group/9',9,'2022-11-02 01:31:03','Available'),
  ('Gyser Installation','http://handyman/one',17,'2022-07-02 20:44:38','Available'),
  ('Phone repairs','https://handyman/one',7,'2021-12-12 18:10:13','Unavailable'),
  ('Computer repairs','http://handyman/en-ca',11,'2022-07-16 15:20:37','Unavailable'),
  ('Lights installation','http://handyman/en-us',34,'2023-01-10 03:22:22','Out-of-service'),
  ('Car repairs','https://handyman/group/9',35,'2022-04-12 12:46:03','Out-of-service'),
  ('Tacth Building','https://handyman/en-us',32,'2022-11-05 19:19:58','Available'),
  ('Sink repairs','https://handyman/user/110',8,'2022-06-09 12:37:54','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Stove repairs','http://handyman/sub',17,'2022-11-09 06:30:32','Unavailable'),
  ('Microwave repairs','http://handyman/settings',36,'2022-02-11 17:43:11','Unavailable'),
  ('Fridge repairs','http://handyman/one',7,'2022-04-18 18:00:35','Out-of-service'),
  ('Ipad repairs','https://handyman/fr',11,'2022-02-15 14:40:54','Out-of-service'),
  ('Tile laying','https://handyman/one',24,'2023-02-16 22:53:24','Available'),
  ('Demolition','http://handyman/sub',14,'2023-04-06 14:11:10','Available'),
  ('Renovation','http://handyman/en-us',7,'2022-08-26 18:02:09','Unavailable'),
  ('House Cleaning','https://handyman/sub/cars',6,'2022-11-10 18:57:44','Unavailable'),
  ('Walls painting','http://handyman/sub',2,'2022-06-03 04:08:19','Out-of-service'),
  ('Welding','https://handyman/one',5,'2023-08-19 00:18:21','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Swimming pool','https://handyman/one',37,'2022-11-11 17:49:39','Available'),
  ('Grass cutting','http://handyman/one',3,'2023-02-17 04:02:18','Available'),
  ('Bush trimming','https://handyman/settings',22,'2023-03-22 23:11:08','Unavailable'),
  ('Paiving','http://handyman/user/110',12,'2022-06-11 04:00:45','Unavailable'),
  ('Gyser Installation','https://handyman/user/110',36,'2021-12-21 03:06:57','Out-of-service'),
  ('Phone repairs','http://handyman/group/9',28,'2023-04-17 23:18:59','Out-of-service'),
  ('Computer repairs','https://handyman/one',35,'2023-01-18 04:29:52','Available'),
  ('Lights installation','https://handyman/site',22,'2022-04-19 10:51:00','Available'),
  ('Car repairs','http://handyman/user/110',12,'2023-09-04 08:50:07','Unavailable'),
  ('Tacth Building','http://handyman/user/110',17,'2022-12-22 17:10:56','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Sink repairs','http://handyman/en-us',22,'2022-11-19 03:55:56','Out-of-service'),
  ('Stove repairs','https://handyman/group/9',33,'2023-03-14 05:45:55','Out-of-service'),
  ('Microwave repairs','http://handyman/sub/cars',28,'2022-08-24 09:29:12','Available'),
  ('Fridge repairs','https://handyman/sub',8,'2023-06-02 09:40:14','Available'),
  ('Ipad repairs','https://handyman/fr',12,'2022-12-28 14:20:05','Unavailable'),
  ('Tile laying','https://handyman/one',12,'2022-01-29 14:29:54','Unavailable'),
  ('Demolition','http://handyman/en-ca',20,'2022-11-11 22:06:41','Out-of-service'),
  ('Renovation','https://handyman/sub/cars',12,'2021-12-30 15:40:09','Out-of-service'),
  ('House Cleaning','https://handyman/user/110',5,'2022-11-24 20:38:09','Available'),
  ('Walls painting','https://handyman/fr',28,'2021-12-29 15:21:00','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Welding','http://handyman/settings',19,'2022-11-12 07:29:11','Unavailable'),
  ('Swimming pool','https://handyman/settings',25,'2021-10-16 18:04:17','Unavailable'),
  ('Grass cutting','http://handyman/user/110',30,'2022-07-13 09:44:51','Out-of-service'),
  ('Bush trimming','https://handyman/fr',39,'2022-09-03 13:27:20','Out-of-service'),
  ('Paiving','http://handyman/fr',31,'2022-11-22 14:05:18','Available'),
  ('Gyser Installation','https://handyman/en-us',35,'2022-11-27 00:35:00','Available'),
  ('Phone repairs','http://handyman/settings',15,'2023-03-27 20:54:25','Unavailable'),
  ('Computer repairs','http://handyman/user/110',14,'2023-08-27 02:56:42','Unavailable'),
  ('Lights installation','https://handyman/en-us',25,'2021-12-12 17:59:08','Out-of-service'),
  ('Car repairs','http://handyman/user/110',37,'2023-01-24 08:01:27','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Tacth Building','https://handyman/fr',38,'2022-09-20 11:20:28','Available'),
  ('Sink repairs','https://handyman/user/110',1,'2023-01-21 23:08:04','Available'),
  ('Stove repairs','https://handyman/group/9',36,'2022-09-04 20:08:38','Unavailable'),
  ('Microwave repairs','http://handyman/fr',13,'2023-08-07 21:48:56','Unavailable'),
  ('Fridge repairs','http://handyman/one',31,'2023-02-02 13:04:10','Out-of-service'),
  ('Ipad repairs','https://handyman/one',1,'2022-01-19 16:10:42','Out-of-service'),
  ('Tile laying','http://handyman/site',26,'2022-08-11 10:55:46','Available'),
  ('Demolition','https://handyman/sub/cars',6,'2021-11-29 21:38:57','Available'),
  ('Renovation','http://handyman/en-us',37,'2022-03-28 19:09:56','Unavailable'),
  ('House Cleaning','http://handyman/site',28,'2023-03-18 10:04:33','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Walls painting','https://handyman/user/110',16,'2022-10-31 14:36:07','Out-of-service'),
  ('Welding','https://handyman/sub/cars',9,'2023-02-18 18:10:21','Out-of-service'),
  ('Swimming pool','https://handyman/fr',39,'2022-12-11 11:47:15','Available'),
  ('Grass cutting','http://handyman/user/110',19,'2022-06-08 20:59:35','Available'),
  ('Bush trimming','https://handyman/en-ca',31,'2021-10-17 01:15:00','Unavailable'),
  ('Paiving','http://handyman/group/9',40,'2022-08-03 03:29:56','Unavailable'),
  ('Gyser Installation','http://handyman/en-ca',12,'2022-12-11 06:39:02','Out-of-service'),
  ('Phone repairs','https://handyman/sub',9,'2022-07-30 13:30:36','Out-of-service'),
  ('Computer repairs','http://handyman/en-ca',27,'2022-09-25 21:52:37','Available'),
  ('Lights installation','http://handyman/user/110',33,'2021-10-21 12:59:03','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Car repairs','http://handyman/one',18,'2022-07-08 21:27:54','Unavailable'),
  ('Tacth Building','http://handyman/group/9',33,'2022-06-25 06:56:01','Unavailable'),
  ('Sink repairs','https://handyman/settings',4,'2022-09-08 06:37:04','Out-of-service'),
  ('Stove repairs','http://handyman/sub/cars',20,'2022-12-11 12:15:43','Out-of-service'),
  ('Microwave repairs','https://handyman/sub',24,'2023-09-11 17:53:38','Available'),
  ('Fridge repairs','https://handyman/group/9',36,'2022-11-24 17:50:21','Available'),
  ('Ipad repairs','http://handyman/fr',37,'2022-05-29 04:02:09','Unavailable'),
  ('Tile laying','http://handyman/sub/cars',31,'2023-01-20 00:09:47','Unavailable'),
  ('Demolition','https://handyman/en-ca',8,'2023-06-20 14:43:20','Out-of-service'),
  ('Renovation','http://handyman/group/9',6,'2022-07-27 11:49:53','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('House Cleaning','http://handyman/one',12,'2021-11-19 11:18:59','Available'),
  ('Walls painting','https://handyman/group/9',1,'2023-01-25 03:01:07','Available'),
  ('Welding','https://handyman/fr',14,'2023-05-19 02:23:22','Unavailable'),
  ('Swimming pool','http://handyman/en-us',16,'2022-03-30 12:58:12','Unavailable'),
  ('Grass cutting','https://handyman/user/110',27,'2023-02-08 12:06:14','Out-of-service'),
  ('Bush trimming','https://handyman/user/110',27,'2023-09-16 03:03:03','Out-of-service'),
  ('Paiving','http://handyman/fr',18,'2022-06-09 20:54:19','Available'),
  ('Gyser Installation','http://handyman/en-ca',39,'2022-04-14 21:07:40','Available'),
  ('Phone repairs','https://handyman/one',36,'2022-01-03 04:10:30','Unavailable'),
  ('Computer repairs','https://handyman/group/9',11,'2022-03-21 05:43:31','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Lights installation','http://handyman/settings',6,'2022-08-13 11:21:50','Out-of-service'),
  ('Car repairs','https://handyman/fr',40,'2021-10-22 01:11:47','Out-of-service'),
  ('Tacth Building','http://handyman/one',5,'2023-09-27 10:12:34','Available'),
  ('Sink repairs','http://handyman/sub/cars',30,'2023-08-23 11:03:34','Available'),
  ('Stove repairs','http://handyman/sub',10,'2021-11-22 03:59:29','Unavailable'),
  ('Microwave repairs','https://handyman/user/110',23,'2023-07-19 21:39:28','Unavailable'),
  ('Fridge repairs','https://handyman/en-ca',24,'2023-06-16 03:13:21','Out-of-service'),
  ('Ipad repairs','https://handyman/sub',28,'2023-07-24 18:37:20','Out-of-service'),
  ('Tile laying','https://handyman/user/110',37,'2022-04-22 11:57:15','Available'),
  ('Demolition','http://handyman/en-us',2,'2023-04-28 05:41:52','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Renovation','https://handyman/en-ca',39,'2022-01-03 08:13:32','Unavailable'),
  ('House Cleaning','https://handyman/fr',19,'2021-10-27 23:48:05','Unavailable'),
  ('Walls painting','http://handyman/site',7,'2023-03-11 16:09:26','Out-of-service'),
  ('Welding','http://handyman/group/9',16,'2022-04-03 18:10:01','Out-of-service'),
  ('Swimming pool','https://handyman/settings',38,'2023-05-02 17:20:36','Available'),
  ('Grass cutting','http://handyman/sub',8,'2023-04-06 06:47:56','Available'),
  ('Bush trimming','https://handyman/fr',16,'2022-06-13 18:53:48','Unavailable'),
  ('Paiving','https://handyman/user/110',23,'2021-11-17 04:18:00','Unavailable'),
  ('Gyser Installation','https://handyman/site',3,'2022-03-22 15:52:31','Out-of-service'),
  ('Phone repairs','http://handyman/sub',17,'2023-03-25 16:35:56','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Computer repairs','https://handyman/sub',10,'2023-06-22 17:14:02','Available'),
  ('Lights installation','https://handyman/settings',7,'2023-01-01 18:14:53','Available'),
  ('Car repairs','http://handyman/sub',27,'2022-01-12 12:30:00','Unavailable'),
  ('Tacth Building','https://handyman/en-us',37,'2022-08-26 06:05:34','Unavailable'),
  ('Sink repairs','https://handyman/one',29,'2022-09-10 02:18:20','Out-of-service'),
  ('Stove repairs','https://handyman/one',4,'2022-07-25 07:55:01','Out-of-service'),
  ('Microwave repairs','https://handyman/user/110',11,'2022-07-11 08:57:36','Available'),
  ('Fridge repairs','http://handyman/group/9',22,'2022-04-30 23:28:57','Available'),
  ('Ipad repairs','https://handyman/site',21,'2023-09-24 10:15:11','Unavailable'),
  ('Tile laying','https://handyman/fr',15,'2022-05-22 10:00:13','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Demolition','https://handyman/sub',11,'2022-07-02 12:21:27','Out-of-service'),
  ('Renovation','http://handyman/site',33,'2023-07-20 00:15:13','Out-of-service'),
  ('House Cleaning','http://handyman/settings',11,'2022-01-18 13:42:16','Available'),
  ('Walls painting','https://handyman/site',11,'2023-06-04 07:39:15','Available'),
  ('Welding','http://handyman/en-us',30,'2022-07-22 10:56:50','Unavailable'),
  ('Swimming pool','https://handyman/user/110',14,'2023-05-29 23:28:42','Unavailable'),
  ('Grass cutting','https://handyman/user/110',28,'2023-03-30 03:59:51','Out-of-service'),
  ('Bush trimming','http://handyman/sub',3,'2022-02-20 01:57:29','Out-of-service'),
  ('Paiving','https://handyman/fr',25,'2022-01-16 07:49:50','Available'),
  ('Gyser Installation','http://handyman/site',10,'2023-08-11 11:39:38','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Phone repairs','https://handyman/one',29,'2023-04-26 03:11:16','Unavailable'),
  ('Computer repairs','http://handyman/settings',32,'2023-04-27 22:47:31','Unavailable'),
  ('Lights installation','http://handyman/group/9',14,'2023-03-23 02:03:43','Out-of-service'),
  ('Car repairs','https://handyman/group/9',2,'2023-05-12 11:06:40','Out-of-service'),
  ('Tacth Building','https://handyman/fr',3,'2022-05-15 04:29:16','Available'),
  ('Sink repairs','http://handyman/group/9',26,'2023-02-18 10:00:51','Available'),
  ('Stove repairs','http://handyman/user/110',32,'2023-09-08 08:38:35','Unavailable'),
  ('Microwave repairs','http://handyman/settings',27,'2022-04-28 09:34:24','Unavailable'),
  ('Fridge repairs','http://handyman/sub/cars',5,'2022-01-12 12:45:48','Out-of-service'),
  ('Ipad repairs','https://handyman/fr',30,'2022-11-25 15:05:10','Out-of-service');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Tile laying','http://handyman/sub',2,'2023-03-28 22:44:19','Available'),
  ('Demolition','https://handyman/settings',9,'2022-04-23 00:18:13','Available'),
  ('Renovation','https://handyman/en-us',15,'2023-09-01 08:11:29','Unavailable'),
  ('House Cleaning','http://handyman/fr',3,'2023-07-11 06:50:46','Unavailable'),
  ('Walls painting','https://handyman/user/110',31,'2023-03-28 19:47:42','Out-of-service'),
  ('Welding','https://handyman/settings',16,'2023-08-16 07:49:00','Out-of-service'),
  ('Swimming pool','https://handyman/one',3,'2022-12-19 04:06:08','Available'),
  ('Grass cutting','http://handyman/sub',30,'2022-07-04 18:32:13','Available'),
  ('Bush trimming','http://handyman/en-us',2,'2022-06-21 00:48:46','Unavailable'),
  ('Paiving','https://handyman/site',31,'2022-04-02 10:34:36','Unavailable');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Gyser Installation','https://handyman/sub/cars',14,'2022-03-29 11:13:29','Out-of-service'),
  ('Phone repairs','https://handyman/sub',12,'2023-04-29 16:54:41','Out-of-service'),
  ('Computer repairs','http://handyman/sub/cars',13,'2022-12-06 15:03:53','Available'),
  ('Lights installation','https://handyman/one',14,'2022-03-31 19:35:18','Available'),
  ('Car repairs','http://handyman/en-us',8,'2023-09-05 20:31:22','Unavailable'),
  ('Tacth Building','http://handyman/settings',32,'2022-09-03 00:32:22','Unavailable'),
  ('Sink repairs','https://handyman/sub',24,'2022-09-10 04:38:10','Out-of-service'),
  ('Stove repairs','http://handyman/en-us',26,'2023-02-14 10:44:23','Out-of-service'),
  ('Microwave repairs','https://handyman/settings',17,'2022-06-18 19:07:28','Available'),
  ('Fridge repairs','http://handyman/sub/cars',23,'2022-06-15 16:05:45','Available');
INSERT INTO [Service].[service]  (serv_name,serv_img,serv_categoryid,serv_datecreated,serv_status)
VALUES
  ('Ipad repairs','https://handyman/sub/cars',23,'2022-10-04 04:30:21','Unavailable'),
  ('Tile laying','https://handyman/sub/cars',10,'2023-08-29 17:14:12','Unavailable'),
  ('Demolition','http://handyman/settings',26,'2022-11-03 12:02:08','Out-of-service'),
  ('Renovation','https://handyman/fr',20,'2022-09-05 17:32:08','Out-of-service'),
  ('House Cleaning','http://handyman/group/9',2,'2022-06-08 01:43:56','Available'),
  ('Walls painting','http://handyman/fr',25,'2022-10-14 00:54:54','Available'),
  ('Welding','http://handyman/user/110',9,'2023-04-01 20:37:26','Unavailable'),
  ('Swimming pool','http://handyman/sub',33,'2021-10-16 04:55:39','Unavailable'),
  ('Grass cutting','https://handyman/sub/cars',7,'2023-02-15 14:54:32','Out-of-service'),
  ('Bush trimming','https://handyman/settings',12,'2023-01-27 15:55:15','Out-of-service');
