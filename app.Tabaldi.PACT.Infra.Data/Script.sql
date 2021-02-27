IF DB_ID('FisioPACTApp') IS NULL
BEGIN
	CREATE DATABASE [FisioPACTApp]
END
GO

USE [FisioPACTApp]

IF OBJECT_ID('Clients', 'U') IS NULL
BEGIN
	CREATE TABLE Clients
	(
		[ID] INT IDENTITY(1,1) NOT NULL,
		[Name] VARCHAR(255) NOT NULL,
		[Phone] VARCHAR(255) NULL,
		[DateOfBirth] DATETIME NULL,
		[Diagnosis] VARCHAR(5000) NULL,
		[Objective] VARCHAR(5000) NULL,
		[ChargingType] TINYINT NOT NULL,
		[Value] DECIMAL(4,2) NOT NULL,
		[HasServiceOnMonday] BIT NOT NULL,
        [StartMonday] DATETIME NULL,
        [EndMonday] DATETIME NULL,
        [HasServiceOnTuesday] BIT NOT NULL,
        [StartTuesday] DATETIME NULL,
        [EndTuesday] DATETIME NULL,
        [HasServiceOnWednesday] BIT NOT NULL,
        [StartWednesday ]DATETIME NULL,
        [EndWednesday] DATETIME NULL,
        [HasServiceOnThursday] BIT NOT NULL,
        [StartThursday] DATETIME NULL,
        [EndThursday] DATETIME NULL,
        [HasServiceOnFriday] BIT NOT NULL,
        [StartFriday] DATETIME NULL,
        [EndFriday] DATETIME NULL,
        [HasServiceOnSaturday] BIT NOT NULL,
        [StartSaturday] DATETIME NULL,
        [EndSaturday] DATETIME NULL,
        [HasServiceOnSunday] BIT NOT NULL,
        [StartSunday] DATETIME NULL,
        [EndSunday] DATETIME NULL,
		[RegistrationDate] DATETIME NOT NULL,
		CONSTRAINT PK_Clients PRIMARY KEY (ID)
	)
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
		CONSTRAINT PK_Attendance PRIMARY KEY (ID)
	)

	ALTER TABLE Attendances ADD FOREIGN KEY ([ClientID]) REFERENCES Clients(ID);
END
GO