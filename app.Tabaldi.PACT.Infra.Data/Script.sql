IF DB_ID('FisioPACTApp') IS NULL
BEGIN
	CREATE DATABASE [FisioPACTApp]
END
GO

USE [FisioPACTApp]
GO

IF OBJECT_ID('Users', 'U') IS NULL
BEGIN
	CREATE TABLE Users
	(
		[ID] INT IDENTITY(1,1) NOT NULL,
		[UserName] VARCHAR(255) NOT NULL,
		[Password] VARCHAR(255) NOT NULL,
		[FullName] VARCHAR(255) NULL,
		[Mail] VARCHAR(255) NULL,
		[SendAlerts] BIT NOT NULL,
		[RegistrationDate] DATETIMEOFFSET NULL,
		CONSTRAINT PK_Users PRIMARY KEY (ID)
	)
END
GO

IF OBJECT_ID('Clients', 'U') IS NULL
BEGIN
	CREATE TABLE Clients
	(
		[ID] INT IDENTITY(1,1) NOT NULL,
		[UserID] INT NOT NULL,
		[Name] VARCHAR(255) NOT NULL,
		[Phone] VARCHAR(255) NULL,
		[DateOfBirth] DATETIME NULL,
		[Value] DECIMAL(10,2) NOT NULL,
		[ChargingType] TINYINT NOT NULL,
		[ClinicalDiagnosis] VARCHAR(5000) NULL,
		[PhysiotherapeuticDiagnosis] VARCHAR(5000) NULL,
		[Objectives] VARCHAR(5000) NULL,
		[TreatmentConduct] VARCHAR(5000) NULL,
		[RegistrationDate] DATETIMEOFFSET NULL,
		CONSTRAINT PK_Clients PRIMARY KEY (ID)
	)

	ALTER TABLE Clients ADD FOREIGN KEY ([UserID]) REFERENCES Users(ID);
END
GO

IF OBJECT_ID('AttendancesRecurrences', 'U') IS NULL
BEGIN
	CREATE TABLE AttendancesRecurrences
	(
		[ID] INT IDENTITY(1,1) NOT NULL,
		[ClientID] INT NOT NULL,
		[WeekDay] TINYINT NOT NULL,
		[StartTime] DATETIME NOT NULL,
		[EndTime] DATETIME NOT NULL
		CONSTRAINT PK_AttendancesRecurrences PRIMARY KEY (ID)
	)

	ALTER TABLE AttendancesRecurrences ADD FOREIGN KEY ([ClientID]) REFERENCES Clients(ID);
END
GO

IF OBJECT_ID('Attendances', 'U') IS NULL
BEGIN
	CREATE TABLE Attendances
	(
		[ID] INT IDENTITY(1,1) NOT NULL,
		[ClientID] INT NOT NULL,
		[Date] DATETIME NOT NULL,
		[HourInitial] DATETIME NOT NULL,
		[HourFinish] DATETIME NOT NULL,
		[Description] VARCHAR(5000) NULL,
		[AlertHasSend] BIT NOT NULL,
		CONSTRAINT PK_Attendance PRIMARY KEY (ID)
	)

	ALTER TABLE Attendances ADD FOREIGN KEY ([ClientID]) REFERENCES Clients(ID);
END
GO

IF NOT EXISTS(SELECT 1 FROM Users)
BEGIN
	INSERT INTO [dbo].[Users]
           ([UserName]
           ,[Password]
           ,[FullName]
           ,[Mail]
           ,[SendAlerts]
           ,[RegistrationDate])
     VALUES
           ('patricia.caroline'
           ,'321'
           ,'Patricia Caroline'
           ,'patriciacaroline977@gmail.com'
           ,1
           ,GETDATE())

	INSERT INTO [dbo].[Users]
           ([UserName]
           ,[Password]
           ,[FullName]
           ,[Mail]
           ,[SendAlerts]
           ,[RegistrationDate])
     VALUES
           ('anderson.tabaldi'
           ,'321'
           ,'Anderson Tabaldi'
           ,'tabaldi98@gmail.com'
           ,1
           ,GETDATE())
END