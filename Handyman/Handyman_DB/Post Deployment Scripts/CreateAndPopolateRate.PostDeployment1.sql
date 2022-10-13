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

IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('[Service].[rating]'))
BEGIN;
    DROP TABLE [Service].[rating];
END;
GO

CREATE TABLE [Service].[rating] (
    [rate_id] INTEGER NOT NULL IDENTITY(1, 1),
    [rate_comment] VARCHAR(MAX) NULL,
    [rate_datecreated] VARCHAR(255) NULL,
    [rate_scale] VARCHAR(MAX) NULL,
    PRIMARY KEY ([rate_id])
);
GO

INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('arcu ac orci. Ut','2021-11-29 03:03:00',1),
  ('fermentum convallis ligula. Donec luctus aliquet odio. Etiam','2021-03-15 12:20:16',2),
  ('montes, nascetur ridiculus mus. Donec','2020-10-28 12:20:21',3),
  ('vel, mauris. Integer sem elit,','2020-09-24 07:50:04',4),
  ('ipsum nunc id','2019-12-22 01:02:24',5),
  ('Sed auctor odio','2022-01-27 12:12:40',6),
  ('turpis egestas. Fusce aliquet magna a neque.','2022-09-23 18:22:33',7),
  ('Suspendisse aliquet, sem ut cursus luctus, ipsum leo','2022-03-06 18:05:32',8),
  ('magna sed dui. Fusce aliquam,','2021-09-23 12:53:50',9),
  ('enim non nisi. Aenean','2021-09-19 09:17:12',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('semper auctor. Mauris vel turpis. Aliquam adipiscing lobortis risus. In','2022-08-25 19:29:11',1),
  ('Curabitur dictum. Phasellus in','2021-09-24 23:38:42',2),
  ('non, luctus sit amet, faucibus ut, nulla.','2020-05-18 00:18:57',3),
  ('ac arcu.','2021-01-29 04:55:50',4),
  ('consectetuer rhoncus. Nullam velit','2019-12-18 22:37:40',5),
  ('Mauris vel turpis. Aliquam','2022-09-15 23:19:28',6),
  ('lacus. Ut nec urna et arcu','2020-11-15 15:24:43',7),
  ('malesuada id, erat.','2021-08-26 14:51:50',8),
  ('magna. Ut tincidunt orci quis lectus. Nullam','2022-02-24 05:36:27',9),
  ('et risus. Quisque libero lacus, varius','2022-02-25 18:57:30',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('sit amet diam eu dolor egestas','2020-03-22 18:07:20',1),
  ('Curabitur ut odio vel','2022-10-11 17:43:46',2),
  ('est. Nunc laoreet lectus quis','2022-01-31 20:02:41',3),
  ('tristique aliquet. Phasellus fermentum convallis ligula. Donec luctus aliquet odio.','2020-02-19 03:44:29',4),
  ('sem ut cursus','2021-09-21 05:55:31',5),
  ('pellentesque eget, dictum placerat, augue.','2022-08-28 20:56:04',6),
  ('congue, elit sed consequat auctor, nunc nulla','2020-12-17 10:39:26',7),
  ('id ante dictum cursus. Nunc mauris','2022-06-24 01:20:31',8),
  ('sit amet, dapibus id, blandit at, nisi.','2022-04-20 01:36:13',9),
  ('ac mattis','2021-11-06 05:41:29',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('ligula eu enim. Etiam','2020-03-08 21:39:54',1),
  ('Nam nulla magna, malesuada vel, convallis in, cursus et,','2021-07-15 06:35:20',2),
  ('magna. Duis dignissim','2020-12-30 16:28:46',3),
  ('Nunc mauris elit, dictum eu, eleifend nec, malesuada ut,','2020-07-28 15:41:21',4),
  ('purus, in molestie tortor','2019-11-15 03:25:47',5),
  ('turpis. In condimentum. Donec at','2021-06-05 10:42:37',6),
  ('orci.','2021-09-22 02:21:45',7),
  ('molestie. Sed','2020-09-13 23:37:35',8),
  ('pede. Cum sociis natoque penatibus et','2020-07-01 18:12:57',9),
  ('faucibus lectus, a sollicitudin orci','2021-08-06 23:47:22',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('vulputate,','2022-08-06 07:41:50',1),
  ('Morbi vehicula. Pellentesque tincidunt tempus risus.','2021-01-10 02:10:27',2),
  ('id, ante. Nunc mauris sapien, cursus in,','2021-02-14 00:25:45',3),
  ('consequat dolor vitae dolor. Donec fringilla.','2021-04-03 21:52:41',4),
  ('posuere cubilia Curae Phasellus ornare. Fusce mollis. Duis sit','2021-03-15 23:08:53',5),
  ('semper et, lacinia vitae, sodales','2021-12-17 19:47:43',6),
  ('in faucibus orci luctus et ultrices','2020-06-22 09:17:27',7),
  ('molestie sodales.','2019-12-20 04:06:59',8),
  ('elit. Curabitur','2021-07-21 19:36:15',9),
  ('felis purus ac tellus. Suspendisse sed dolor. Fusce mi','2021-12-05 08:34:38',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('egestas. Aliquam nec enim. Nunc ut','2021-04-20 03:03:38',1),
  ('amet ante. Vivamus','2019-11-16 08:53:57',2),
  ('vehicula et, rutrum eu, ultrices','2022-06-04 07:06:05',3),
  ('aliquet. Proin velit. Sed malesuada augue','2022-04-01 00:08:42',4),
  ('dapibus','2021-04-22 20:54:59',5),
  ('justo. Proin non massa non ante bibendum ullamcorper.','2020-03-06 20:24:48',6),
  ('amet massa. Quisque porttitor eros nec tellus. Nunc lectus pede,','2021-04-11 10:30:49',7),
  ('ut quam vel sapien imperdiet ornare. In','2021-11-18 02:46:12',8),
  ('laoreet ipsum. Curabitur','2021-07-20 10:53:18',9),
  ('tincidunt adipiscing. Mauris molestie','2021-02-16 04:44:24',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('nisl. Nulla eu neque pellentesque','2022-01-07 19:48:28',1),
  ('Mauris ut quam vel sapien imperdiet ornare. In faucibus.','2020-01-19 05:12:18',2),
  ('elit. Aliquam auctor, velit eget laoreet posuere, enim','2021-01-21 16:11:10',3),
  ('et magnis','2021-08-01 21:29:07',4),
  ('nec tellus. Nunc lectus pede, ultrices a, auctor non,','2020-09-17 00:40:03',5),
  ('nibh. Phasellus nulla. Integer vulputate, risus','2020-11-28 05:48:22',6),
  ('at pede. Cras vulputate velit eu sem.','2019-10-30 03:55:24',7),
  ('orci lobortis augue scelerisque mollis. Phasellus libero mauris,','2021-10-25 18:56:38',8),
  ('Nunc sollicitudin commodo ipsum. Suspendisse non','2021-07-13 02:28:29',9),
  ('Nulla interdum. Curabitur dictum. Phasellus in felis.','2022-09-04 08:58:37',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('tellus sem mollis','2021-12-26 22:16:16',1),
  ('feugiat metus sit amet ante. Vivamus','2020-09-25 20:07:55',2),
  ('faucibus orci luctus','2019-11-01 11:30:43',3),
  ('torquent per conubia nostra, per','2022-05-10 06:03:50',4),
  ('interdum feugiat. Sed nec metus','2021-01-18 22:14:56',5),
  ('dolor elit, pellentesque a,','2020-02-17 21:24:30',6),
  ('risus quis diam luctus lobortis. Class aptent taciti','2022-03-07 10:18:22',7),
  ('eu','2022-03-12 03:20:55',8),
  ('vel, mauris. Integer sem elit, pharetra ut, pharetra sed,','2021-07-08 09:04:48',9),
  ('Donec egestas. Aliquam nec','2020-02-04 20:30:48',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('euismod enim. Etiam gravida molestie arcu.','2021-12-06 16:10:44',1),
  ('Nulla facilisi. Sed neque. Sed eget','2022-08-23 23:02:21',2),
  ('dui.','2022-08-25 23:37:06',3),
  ('quis turpis vitae purus gravida sagittis. Duis gravida. Praesent','2020-07-09 18:39:37',4),
  ('sapien. Nunc pulvinar arcu','2022-09-14 21:09:44',5),
  ('enim. Sed nulla','2021-02-06 22:54:04',6),
  ('Morbi neque tellus, imperdiet','2022-06-26 23:04:34',7),
  ('vitae, erat. Vivamus nisi. Mauris nulla. Integer urna.','2020-10-20 16:43:55',8),
  ('lectus pede, ultrices','2020-01-04 14:13:39',9),
  ('tincidunt pede ac urna. Ut','2021-08-17 05:35:09',10);
INSERT INTO [Service].[rating] (rate_comment,rate_datecreated,rate_scale)
VALUES
  ('in, cursus et,','2021-05-13 22:32:46',1),
  ('ante. Vivamus','2021-03-10 16:34:45',2),
  ('elit. Aliquam auctor, velit eget','2020-05-19 11:07:01',3),
  ('vestibulum. Mauris magna. Duis dignissim tempor arcu. Vestibulum','2020-01-27 05:21:17',4),
  ('est. Mauris eu turpis. Nulla aliquet.','2022-07-10 13:22:12',5),
  ('justo','2022-02-07 13:03:22',6),
  ('primis in','2021-01-09 21:24:21',7),
  ('sit amet metus. Aliquam erat volutpat.','2021-09-29 09:54:48',8),
  ('Vivamus','2020-11-15 03:00:02',9),
  ('nibh.','2020-04-13 05:57:23',10);
