IF db_id('Series') IS NULL 
    CREATE DATABASE Series
GO

CREATE TABLE Series.dbo.Sequences
(
	Id bigint NOT NULL IDENTITY (1,1) PRIMARY KEY,
	SeqType int NOT NULL,
	Numbers varchar(max) NOT NULL
);
