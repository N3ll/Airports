USE [master]
GO

/****** Object:  Database [ABS]    Script Date: 5.5.2017 �. 12:48:42 ******/
DROP DATABASE [ABS]
GO

/****** Object:  Database [ABS]    Script Date: 5.5.2017 �. 12:48:42 ******/
CREATE DATABASE [ABS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ABS', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ABS.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ABS_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ABS_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [ABS] SET COMPATIBILITY_LEVEL = 110
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ABS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ABS] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ABS] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ABS] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ABS] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ABS] SET ARITHABORT OFF 
GO

ALTER DATABASE [ABS] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ABS] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ABS] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ABS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ABS] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ABS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ABS] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ABS] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ABS] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ABS] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ABS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ABS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ABS] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ABS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ABS] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ABS] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ABS] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ABS] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ABS] SET  MULTI_USER 
GO

ALTER DATABASE [ABS] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ABS] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ABS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ABS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [ABS] SET  READ_WRITE 
GO

use [ABS];

--GO
----drop constrains forign keys

--ALTER TABLE [dbo].[Seat]
--DROP CONSTRAINT FK_FlightSection_Seat;

--ALTER TABLE [dbo].[FlightSection]
--DROP CONSTRAINT FK_SectionClass_FlightSection;

--ALTER TABLE [dbo].[Flight]
--DROP CONSTRAINT FK_Airline_Flight;

--ALTER TABLE [dbo].[Flight]
--DROP CONSTRAINT FK_OriginAirport_Flight;

--ALTER TABLE [dbo].[Flight]
--DROP CONSTRAINT FK_DestinationAirport_Flight;

--ALTER TABLE [dbo].[FlightSection] 
--DROP CONSTRAINT FK_Flight_FlightSection;

--ALTER TABLE [dbo].[FlightFilter]
--DROP CONSTRAINT FK_Flight_FlightFilter;

--ALTER TABLE [dbo].[FlightFilter]
--DROP CONSTRAINT FK_Filter_FlightFilter;

--GO
----drop tables 

--DROP TABLE [dbo].[Airline];
--DROP TABLE [dbo].[Flight];
--DROP TABLE [dbo].[Airport];
--DROP TABLE [dbo].[FlightSection];
--DROP TABLE [dbo].[SectionClass];
--DROP TABLE [dbo].[Seat];
--DROP TABLE [dbo].[Filter];
--DROP TABLE [dbo].[AirportFilter];

--GO

--create the tables

CREATE TABLE [dbo].[Airline](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(6) NOT NULL);

CREATE TABLE [dbo].[Flight](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL,
	[DepartureDate] DATETIME NOT NULL,
	[AirlineId] INT NOT NULL,
	[DestinationAirportId] INT NOT NULL,
	[OriginAirportId] INT NOT NULL,
	[Price] DECIMAL(10,6) NOT NULL,
	CONSTRAINT CHK_Airpots CHECK (DestinationAirportId <> OriginAirportId));

CREATE TABLE [dbo].[Airport](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(3) NOT NULL,
	[Latitude] DECIMAL(10,6) NOT NULL,
	[Longitude]DECIMAL(10,6) NOT NULL);

CREATE TABLE [dbo].[FlightSection](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [SectionClassId] INT NOT NULL,
	[FlightId] INT NOT NULL);

CREATE TABLE [dbo].[SectionClass](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL);

CREATE TABLE [dbo].[Seat](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Row] INT NOT NULL,
	[Col] NVARCHAR(1) NOT NULL,
	[FlightSectionId] INT NOT NULL,
	[Status] BIT NOT NULL DEFAULT 0,
	CONSTRAINT CHK_Row CHECK (Row>=1 AND Row<=100),
	CONSTRAINT CHK_Col CHECK(Col IN ('A','B','C','D','E','F','G','H','I','J')));

CREATE TABLE [dbo].[Filter](
	[Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL);

CREATE TABLE [dbo].[AirportFilter](
	[AirportId] INT  NOT NULL,
	[FilterId] INT  NOT NULL,
)

GO
--add constrains primary and foreign keys

ALTER TABLE [dbo].[Airline] ADD PRIMARY KEY (Id);

ALTER TABLE [dbo].[Airport] ADD PRIMARY KEY (Id);

ALTER TABLE [dbo].[SectionClass] ADD PRIMARY KEY (Id);

ALTER TABLE [dbo].[Flight] ADD PRIMARY KEY (Id);
ALTER TABLE [dbo].[Flight] ADD CONSTRAINT FK_Airline_Flight
FOREIGN KEY (AirlineId) REFERENCES Airline(Id);
ALTER TABLE [dbo].[Flight] ADD CONSTRAINT FK_OriginAirport_Flight
FOREIGN KEY (OriginAirportId) REFERENCES Airport(Id);
ALTER TABLE [dbo].[Flight] ADD CONSTRAINT FK_DestinationAirport_Flight
FOREIGN KEY (DestinationAirportId) REFERENCES Airport(Id);

ALTER TABLE [dbo].[FlightSection] ADD PRIMARY KEY (Id);
ALTER TABLE [dbo].[FlightSection] ADD CONSTRAINT FK_SectionClass_FlightSection
FOREIGN KEY (SectionClassId) REFERENCES SectionClass(Id);
ALTER TABLE [dbo].[FlightSection] ADD CONSTRAINT FK_Flight_FlightSection
FOREIGN KEY (FlightId) REFERENCES Flight(Id);


ALTER TABLE [dbo].[Seat] ADD PRIMARY KEY (Id);
ALTER TABLE [dbo].[Seat] ADD CONSTRAINT FK_FlightSection_Seat
FOREIGN KEY (FlightSectionId) REFERENCES FlightSection(Id);

ALTER TABLE [dbo].[Filter] ADD PRIMARY KEY (Id);

ALTER TABLE [dbo].[AirportFilter] ADD PRIMARY KEY (AirportId,FilterId);
ALTER TABLE [dbo].[AirportFilter] ADD CONSTRAINT FK_Airport_FlightFilter
FOREIGN KEY (AirportId) REFERENCES Airport(Id);
ALTER TABLE [dbo].[AirportFilter] ADD CONSTRAINT FK_Filter_FlightFilter
FOREIGN KEY (FilterId) REFERENCES [Filter](Id);

 GO
use ABS;

--IF OBJECT_ID('dbo.FindAvailableFlights','P') IS NOT NULL
--DROP PROC dbo.FindAvailableFlights;
GO

CREATE PROC dbo.FindAvailableFlights
	@originAirport AS NVARCHAR(3),
	@destinationAirport AS NVARCHAR(3),
	@departureDate AS DATETIME

AS
SET NOCOUNT ON;

SELECT DISTINCT F.[Name] AS FlightName,AL.[Name] AS AirlineName,A.[Name] As OriginAirport,A2.[Name] AS DestinationAIrport,F.[DepartureDate], F.[Price]
FROM [Flight] AS F
	JOIN [FlightSection] AS FS
	ON F.Id = FS.FlightId
	JOIN [Airport] AS A
	ON F.OriginAirportId=A.Id
	JOIN [Airport] AS A2
	ON F.DestinationAirportId=A2.Id
	JOIN [Seat] AS S
	ON FS.Id=S.FlightSectionId
	JOIN Airline AS AL
	ON F.AirlineId=AL.Id
WHERE A.[Name]=@originAirport
	AND A2.[Name]=@destinationAIrport
	AND F.DepartureDate=@departureDate
	AND S.[Status] = 0;

GO

CREATE TABLE dbo.Audit
(
Id INT NOT NULL IDENTITY PRIMARY KEY,
BookingDate DATETIME NOT NULL DEFAULT(SYSDATETIME()),
FlightId INT NOT NULL,
SeatId INT NOT NULL
)

GO
CREATE TRIGGER trg_Seat_Update_audit ON dbo.Seat AFTER UPDATE
AS
SET NOCOUNT ON;

INSERT INTO dbo.Audit(FlightId, SeatId)
SELECT f.Id, i.Id 
FROM inserted AS i
JOIN FlightSection AS fs
ON fs.Id = i.FlightSectionId
JOIN Flight as f
ON f.Id = fs.FlightId

GO

CREATE VIEW vSystemDetails
AS
 SELECT f.[Name] AS FlightName, a.[Name] AS AirlineName,oa.[Name] AS OriginAirportName, da.[Name] AS DestinationAirportName, sc.[Name] as SeatClass, s.[Row], s.Col, s.[Status]
 FROM Flight AS f
 JOIN Airport AS oa
 ON f.OriginAirportId=oa.Id
 JOIN Airport AS da
 ON f.DestinationAirportId = da.Id
 JOIN Airline AS a
 ON f.AirlineId=a.Id
 JOIN FlightSection AS fs
 ON f.Id=fs.FlightId
 JOIN SectionClass as sc
 ON sc.Id=fs.SectionClassId
 JOIN Seat as s
 ON s.FlightSectionId=fs.Id
 GO

 use ABS;
--DELETE FROM FlightSection;
--DELETE FROM Seat;
--DELETE FROM SectionClass;
--DELETE FROM [Filter];
--DELETE FROM Flight;
--DELETE FROM Airport;
--DELETE FROM Airline;

--airprots
INSERT INTO Airport([Name], Latitude, Longitude)
VALUES ('LHR',51.4700,0.4543),
('FRA',50.0379,8.5622),
('CDG',49.0097,2.5479),
('AMS',52.3105,4.7683),
('BCN',41.2974,2.0833),
('ZRH',47.4582,8.5555),
('MUC',48.3537,11.7750),
('CPH',55.6180,12.6508),
('NCE',43.6598,7.2148),
('BRU',50.9010,4.4856),
('OSL',60.1976,11.1004),
('TXL',52.5588,13.2884),
('MAD',40.4839,3.5680),
('FCO',41.7999,12.2462),
('IST',40.9829,28.8104),
('ARN',59.6498,17.9238),
('ORY',48.7262,2.3652),
('DUB',53.4264,6.2499),
('VIE',48.1158,16.5666),
('WAW',52.1672,20.9679),
('HEL',60.3210,24.9529),
('HAM',53.6336,9.9974),
('MXP',45.6301,8.7255),
('PRG',50.1018,14.2632),
('EDI',55.9508,3.3615),
('KEF',63.9868,22.6280),
('BUD',47.4385,19.2523),
('PMI',39.5517,2.7362),
('GVA',46.2370,6.1092),
('SOF',42.6894,23.4144),
('OTP',44.5707,26.0844),
('ATH',37.9356,23.9484),
('BLL',55.7408,9.1526),
('VAR',43.2319,27.8250);

--airlines
INSERT INTO Airline([Name])
VALUES ('DELTA'),
('ALPHA'),
('OMEGA');

--section classes
INSERT INTO SectionClass([Name])
VALUES ('First'),
('Economy'),
('Business');

--flight filter
INSERT INTO [Filter]([Name])
VALUES ('Sun & sea'),
('Snow & ski'),
('Big city'),
('Everywhere');

--flights
INSERT INTO Flight([Name],DepartureDate,AirlineId,OriginAirportId,DestinationAirportId,Price)
VALUES ('A01','20170601',2,33,1,59.99),
('A02','20170601',2,33,2,59.99),
('A03','20170601',2,33,3,59.99),
('A21','20170601',1,33,4,59.99),
('A22','20170601',1,33,5,59.99),
('A23','20170601',1,33,6,59.99),
('A31','20170601',3,33,7,59.99),
('A32','20170601',3,33,8,59.99),
('A33','20170601',3,33,9,59.99),
('A34','20170601',3,33,10,59.99);

INSERT INTO Flight([Name],DepartureDate,AirlineId,OriginAirportId,DestinationAirportId,Price)
VALUES ('B01','20170601',2,8,10,59.99),
('B02','20170601',2,8,11,59.99),
('B03','20170601',2,8,12,59.99),
('B21','20170601',1,8,13,59.99),
('B22','20170601',1,8,14,59.99),
('B23','20170601',1,8,15,59.99),
('B31','20170601',3,8,16,59.99),
('B32','20170601',3,8,17,59.99),
('B33','20170601',3,8,18,59.99),
('B34','20170601',3,8,19,59.99);

INSERT INTO Flight([Name],DepartureDate,AirlineId,OriginAirportId,DestinationAirportId,Price)
VALUES ('C01','20170601',2,34,20,59.99),
('C02','20170601',2,34,21,59.99),
('C03','20170601',2,34,22,59.99),
('C21','20170601',1,34,23,59.99),
('C22','20170601',1,34,24,59.99),
('C23','20170601',1,34,25,59.99),
('C31','20170601',3,34,26,59.99),
('C32','20170601',3,34,27,59.99),
('C33','20170601',3,34,28,59.99),
('C34','20170601',3,34,29,59.99);

--flight sections
INSERT INTO FlightSection(SectionClassId,FlightId)
VALUES (2,1),(2,2),(2,3),(2,4),(2,5),(2,6),(2,7),(2,8),(2,9),(2,10),(2,11),(2,12),(2,13),(2,14),(2,15),(2,16),(2,17),(2,18),(2,19),(2,20),(2,21),(2,22),(2,23),(2,24),(2,25),(2,26),(2,27),(2,28),(2,29),(2,30);

--airport filters
INSERT INTO AirportFilter(AirportId, FilterId)
VALUES (1,3),(1,4),(2,3),(2,4),(3,3),(3,4),(4,3),(4,4),(5,3),(5,4),(5,1),(6,3),(6,4),(6,2),(7,3),(7,4),(8,3),(8,4),(9,4),
(9,1),(10,3),(10,4),(11,4),(12,3),(12,4),(13,4),(13,3),(14,4),(14,3),(15,4),(15,3),(16,4),(16,2),(17,3),(17,4),(18,4),
(18,3),(19,4),(19,3),(20,4),(20,3),(21,4),(21,2),(22,4),(22,3),(23,4),(23,3),(24,3),(24,4),(25,4),(25,3),(26,4),(26,2),
(27,4),(27,3),(28,4),(28,1),(29,4),(29,3),(30,3),(30,4),(31,3),(31,4),(32,3),(32,4),(33,4),(34,4),(34,1);


--seats
INSERT INTO Seat([Row],[Col],FlightSectionId,[Status])
VALUES (1,'A',1,0),(1,'A',2,0),(1,'A',3,0),(1,'A',4,0),(1,'A',5,0),(1,'A',6,0),(1,'A',7,0),(1,'A',8,0),(1,'A',9,0),(1,'A',10,0),(1,'A',11,0),(1,'A',12,0),(1,'A',13,0),(1,'A',14,0),(1,'A',15,0),(1,'A',16,0),(1,'A',17,0),(1,'A',18,0),
(1,'A',19,0),(1,'A',20,0),(1,'A',21,0),(1,'A',22,0),(1,'A',23,0),(1,'A',24,0),(1,'A',25,0),(1,'A',25,0),(1,'A',26,0),
(1,'A',27,0),(1,'A',28,0),(1,'A',29,0);
 GO

