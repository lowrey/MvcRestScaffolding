
-- --------------------------------------------------
-- Date Created: 02/11/2013 14:48:22
-- compatible SQLite
-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

	
	DROP TABLE if exists [Jobs];

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Jobs'
CREATE TABLE [Jobs] (
	[Id] integer PRIMARY KEY AUTOINCREMENT  NOT NULL ,
	[CallbackUrl] nvarchar(8000)   NULL ,
	[Status] smallint   NOT NULL ,
	[Data] nvarchar(2147483647)   NULL 
);


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------